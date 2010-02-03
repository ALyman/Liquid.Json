using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liquid.Json.TypeSerializers {
    class JsonBooleanSerializer : IJsonTypeSerializer<Boolean> {
        public void Serialize(Boolean value, JsonSerializationContext context) {
            context.Writer.WriteValue(value);
        }


        public Boolean Deserialize(JsonDeserializationContext context) {
            string str = context.Reader.ReadNextAs(JsonTokenType.True, JsonTokenType.False).ToLower();
            switch (str) {
                case "true": return true;
                case "false": return false;
                default: throw new JsonDeserializationException();
            }
        }
    }
}
