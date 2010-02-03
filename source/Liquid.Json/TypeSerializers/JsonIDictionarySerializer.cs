using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    class JsonIDictionarySerializer<T> : IJsonTypeSerializer<T> where T : IDictionary {
        public void Serialize(T value, JsonSerializationContext context) {
            context.Writer.WriteStartObject();
            foreach (var key in value.Keys) {
                var item = value[key];
                context.Writer.WriteName(key.ToString());
                if (item == null)
                    context.Serialize(item);
                else {
                    var type = item.GetType();
                    context.SerializeAs(type, item);
                }
            }
            context.Writer.WriteEnd();
        }


        public T Deserialize(JsonDeserializationContext context) {
            throw new NotImplementedException();
        }
    }
    class JsonIDictionarySerializer<T, K, V> : IJsonTypeSerializer<T> where T : IDictionary<K,V> {
        public void Serialize(T value, JsonSerializationContext context) {
            context.Writer.WriteStartObject();
            foreach (var item in value) {
                context.Writer.WriteName(item.Key.ToString());
                context.Serialize<V>(item.Value);
            }
            context.Writer.WriteEnd();
        }


        public T Deserialize(JsonDeserializationContext context) {
            throw new NotImplementedException();
        }
    }
}
