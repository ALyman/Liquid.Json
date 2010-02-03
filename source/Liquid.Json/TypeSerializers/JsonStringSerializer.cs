using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    class JsonStringSerializer : IJsonTypeSerializer<string> {
        public void Serialize(string @object, JsonSerializationContext context) {
            context.Writer.WriteValue(@object);
        }

        public string Deserialize(JsonDeserializationContext context) {
            var str = context.Reader.ReadNextAs(JsonTokenType.String);
            return Json.UnescapeString(str);
        }
    }
}
