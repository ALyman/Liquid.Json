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
            context.Write('[');
            bool first = true;
            foreach (var item in value) {
                if (first)
                    first = false;
                else
                    context.Write(", ");
                if (item == null)
                    context.Serialize(item);
                else {
                    var type = item.GetType();
                    context.SerializeAs(type, item);
                }
            }
            context.Write(']');
        }


        public T Deserialize(JsonDeserializationContext context) {
            throw new NotImplementedException();
        }
    }
    class JsonIEnumerableSerializer<T, S> : IJsonTypeSerializer<T>
        where T : IEnumerable<S> {
        public void Serialize(T value, JsonSerializationContext context) {
            context.Write('[');
            bool first = true;
            foreach (var item in value) {
                if (first)
                    first = false;
                else
                    context.Write(", ");
                context.Serialize<S>(item);
            }
            context.Write(']');
        }


        public T Deserialize(JsonDeserializationContext context) {
            throw new NotImplementedException();
        }
    }

}
