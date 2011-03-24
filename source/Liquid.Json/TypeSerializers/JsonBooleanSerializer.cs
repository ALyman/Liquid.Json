using System;

namespace Liquid.Json.TypeSerializers
{
    internal class JsonBooleanSerializer : IJsonTypeSerializer<Boolean>
    {
        #region IJsonTypeSerializer<bool> Members

        public void Serialize(Boolean @object, JsonSerializationContext context)
        {
            context.Writer.WriteValue(@object);
        }


        public Boolean Deserialize(JsonDeserializationContext context)
        {
            string str = context.Reader.ReadNextAs(JsonTokenType.True, JsonTokenType.False).ToLower();
            switch (str) {
                case "true":
                    return true;
                case "false":
                    return false;
                default:
                    throw new JsonDeserializationException();
            }
        }

        #endregion
    }
}