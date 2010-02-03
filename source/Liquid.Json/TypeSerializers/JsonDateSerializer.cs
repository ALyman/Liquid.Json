using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    public class JsonDateSerializer : IJsonTypeSerializer<DateTime> {
        public void Serialize(DateTime value, TextWriter writer, JsonSerializer serializer) {
            writer.Write("new Date({0})", value.ToFileTime());
        }
    }
}
