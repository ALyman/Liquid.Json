using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    class JsonStringSerializer : IJsonTypeSerializer<string> {
        public void Serialize(string value, JsonSerializationContext context) {
            context.Writer.WriteValue(value);
        }

        public string Deserialize(JsonDeserializationContext context) {
            var str = context.Reader.ReadNextAs(JsonTokenType.String);
            return Json.UnescapeString(str);
        }
    }
}
