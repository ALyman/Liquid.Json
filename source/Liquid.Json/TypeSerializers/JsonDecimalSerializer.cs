using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    class JsonDecimalSerializer : IJsonTypeSerializer<Decimal> {
        public void Serialize(decimal value, TextWriter writer, JsonSerializer serializer) {
            writer.Write(value.ToString(serializer.FormatProvider));
        }
    }
}
