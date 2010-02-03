using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    class JsonStringSerializer : IJsonTypeSerializer<string> {
        public void Serialize(string value, TextWriter writer, JsonSerializer serializer) {
            writer.Write('"');
            foreach (char ch in value) {
                switch (ch) {
                    case '"': writer.Write("\\\""); break;
                    default: writer.Write(ch); break;
                }
            }
            writer.Write('"');
        }
    }
}
