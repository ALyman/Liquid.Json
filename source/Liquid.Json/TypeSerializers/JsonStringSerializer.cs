using System;

namespace Liquid.Json.TypeSerializers
{
    internal class JsonStringSerializer : IJsonTypeSerializer<string>
    {
        #region IJsonTypeSerializer<string> Members

        public void Serialize(string @object, JsonSerializationContext context)
        {
            context.Writer.WriteValue(@object);
        }

        public string Deserialize(JsonDeserializationContext context)
        {
            string str = context.Reader.ReadNextAs(JsonTokenType.String);
            return Json.UnescapeString(str);
        }

        #endregion
    }
}