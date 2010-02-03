using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    class JsonRealSerializer<T> : IJsonTypeSerializer<T>
    where T : IFormattable {
        public void Serialize(T value, TextWriter writer, JsonSerializer serializer) {
            writer.Write(value.ToString(null, serializer.FormatProvider));
        }
    }
}
