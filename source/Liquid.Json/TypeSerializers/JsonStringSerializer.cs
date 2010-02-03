using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    class JsonStringSerializer : IJsonTypeSerializer<string> {
        public void Serialize(string value, JsonSerializationContext context) {
            context.Write('"');
            foreach (char ch in value) {
                switch (ch) {
                    case '"': context.Write("\\\""); break;
                    default: context.Write(ch); break;
                }
            }
            context.Write('"');
        }
    }
}
