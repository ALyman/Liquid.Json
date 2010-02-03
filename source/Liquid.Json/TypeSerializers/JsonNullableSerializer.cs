using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    class JsonNullableSerializer<T, S> : IJsonTypeSerializer<T> where S : struct {
        public void Serialize(T value, JsonSerializationContext context) {
            var n = (Nullable<S>)(object)value;
            if (n.HasValue)
                context.Serialize(n.Value);
            else
                context.Write("null");
        }
    }
}
