using System;
using System.Globalization;

namespace Liquid.Json.TypeSerializers
{
    internal class JsonDateSerializer : IJsonTypeSerializer<DateTime>
    {
        private static readonly DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        #region IJsonTypeSerializer<DateTime> Members

        public void Serialize(DateTime @object, JsonSerializationContext context)
        {
            TimeSpan span = @object.ToUniversalTime() - EPOCH;
            context.Writer.WriteStartConstructor("Date");
            context.Writer.WriteValue((long)span.TotalMilliseconds);
            context.Writer.WriteEnd();
        }

        public DateTime Deserialize(JsonDeserializationContext context)
        {
            context.Reader.ReadNextAs(JsonTokenType.New);
            if (context.Reader.ReadNextAs(JsonTokenType.Identifier) != "Date") {
                throw context.Reader.UnexpectedTokenException("Date", JsonTokenType.Identifier);
            }
            context.Reader.ReadNextAs(JsonTokenType.ConstructorStart);
            var value = context.Reader.ReadNextAs(JsonTokenType.Integer, JsonTokenType.String);
            DateTime result;
            long totalMiliseconds;
            if (Int64.TryParse(value, out totalMiliseconds)) {
                if (context.Reader.ReadNext() && context.Reader.Token == JsonTokenType.Comma) {
                    int year = 0, month = 0, day = 0, hour = 0, minute = 0, second = 0, miliseconds = 0;
                    year = (int)totalMiliseconds;
                    month = Int32.Parse(context.Reader.ReadNextAs(JsonTokenType.Integer));
                    context.Reader.ReadNextAs(JsonTokenType.Comma); // year, month, and day are required.
                    day = Int32.Parse(context.Reader.ReadNextAs(JsonTokenType.Integer));
                    if (!context.Reader.ReadNext() || context.Reader.Token != JsonTokenType.Comma) goto FINISH;
                    hour = Int32.Parse(context.Reader.ReadNextAs(JsonTokenType.Integer));
                    if (!context.Reader.ReadNext() || context.Reader.Token != JsonTokenType.Comma) goto FINISH;
                    minute = Int32.Parse(context.Reader.ReadNextAs(JsonTokenType.Integer));
                    if (!context.Reader.ReadNext() || context.Reader.Token != JsonTokenType.Comma) goto FINISH;
                    second = Int32.Parse(context.Reader.ReadNextAs(JsonTokenType.Integer));
                    if (!context.Reader.ReadNext() || context.Reader.Token != JsonTokenType.Comma) goto FINISH;
                    miliseconds = Int32.Parse(context.Reader.ReadNextAs(JsonTokenType.Integer));
                    context.Reader.ReadNext(); // We must over-read here, since we're going to put it back afterwards anyways
                FINISH:
                    result = new DateTime(year, month, day, hour, minute, second, miliseconds, DateTimeKind.Utc);
                } else {
                    result = EPOCH.AddMilliseconds(totalMiliseconds);
                }
                context.Reader.UndoRead();
            } else {
                var str = Json.UnescapeString(value);
                result = DateTime.ParseExact(str, "MMMM d, yyyy HH:mm:ss zzz", CultureInfo.InvariantCulture);
            }
            context.Reader.ReadNextAs(JsonTokenType.ConstructorEnd);

            return result;
        }

        #endregion
    }
}