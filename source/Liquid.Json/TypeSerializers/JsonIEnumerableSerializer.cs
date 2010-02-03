using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace Liquid.Json.TypeSerializers {
    class JsonIEnumerableSerializer<T, S> : IJsonTypeSerializer<T>
        where T : IEnumerable<S> {
        public void Serialize(T @object, JsonSerializationContext context) {
            context.Writer.WriteStartArray();
            foreach (var item in @object) {
                context.Serialize<S>(item);
            }
            context.Writer.WriteEnd();
        }


        public T Deserialize(JsonDeserializationContext context) {
            throw new NotImplementedException();
        }
    }

}
