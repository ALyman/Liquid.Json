using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    class JsonIntegerSerializer<T> : IJsonTypeSerializer<T> 
        where T : IFormattable {

        public void Serialize(T value, JsonSerializationContext context) {
            context.Write(value.ToString(null, context.FormatProvider));
        }
    }
}
