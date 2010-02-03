using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    class JsonDecimalSerializer : IJsonTypeSerializer<Decimal> {
        public void Serialize(decimal value, JsonSerializationContext context) {
            context.Write(value.ToString(context.FormatProvider));
        }
    }
}
