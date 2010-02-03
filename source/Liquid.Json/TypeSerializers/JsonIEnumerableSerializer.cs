using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace Liquid.Json.TypeSerializers {
    class JsonIEnumerableSerializer<T> : IJsonTypeSerializer<T>
    where T : IEnumerable {
        public void Serialize(T value, JsonSerializationContext context) {
            context.Writer.WriteStartArray();
            foreach (var item in value) {
                if (item == null)
                    context.Serialize(item);
                else 
                    context.SerializeAs(item.GetType(), item);
            }
            context.Writer.WriteEnd();
        }


        public T Deserialize(JsonDeserializationContext context) {
            throw new NotImplementedException();
        }
    }
    class JsonIEnumerableSerializer<T, S> : IJsonTypeSerializer<T>
        where T : IEnumerable<S> {
        public void Serialize(T value, JsonSerializationContext context) {
            context.Writer.WriteStartArray();
            foreach (var item in value) {
                context.Serialize<S>(item);
            }
            context.Writer.WriteEnd();
        }


        public T Deserialize(JsonDeserializationContext context) {
            throw new NotImplementedException();
        }
    }

}
