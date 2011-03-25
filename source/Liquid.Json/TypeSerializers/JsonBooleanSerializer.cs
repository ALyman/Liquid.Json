using System;
using System.Diagnostics;

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
            if (str == "true") {
                return true;
            } else {
                Debug.Assert(str == "false");
                return false;
            }
        }

        #endregion
    }
}