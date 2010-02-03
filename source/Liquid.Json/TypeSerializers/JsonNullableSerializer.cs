using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    class JsonNullableSerializer<T, S> : IJsonTypeSerializer<T>
        where S : struct {
        public void Serialize(T @object, JsonSerializationContext context) {
            var n = (Nullable<S>)(object)@object;
            if (n.HasValue)
                context.Serialize(n.Value);
            else
                context.Writer.WriteNull();
        }


        public T Deserialize(JsonDeserializationContext context) {
            throw new NotImplementedException();
        }
    }
}
