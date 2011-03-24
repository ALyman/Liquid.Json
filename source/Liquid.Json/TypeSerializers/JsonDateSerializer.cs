using System;

namespace Liquid.Json.TypeSerializers
{
    internal class JsonDateSerializer : IJsonTypeSerializer<DateTime>
    {
        private static readonly DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        #region IJsonTypeSerializer<DateTime> Members

        public void Serialize(DateTime @object, JsonSerializationContext context)
        {
            TimeSpan span = @object - EPOCH;
            context.Writer.WriteStartConstructor("Date");
            context.Writer.WriteValue((long) span.TotalSeconds);
            context.Writer.WriteEnd();
        }

        public DateTime Deserialize(JsonDeserializationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}