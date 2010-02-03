using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    class JsonIDictionarySerializer<T> : IJsonTypeSerializer<T> where T : IDictionary {
        public void Serialize(T value, JsonSerializationContext context) {
            context.Write('{');
            bool first = true;
            foreach (var key in value.Keys) {
                if (first)
                    first = false;
                else
                    context.Write(", ");
                var item = value[key];
                context.Serialize(key.ToString());
                context.Write(": ");
                if (item == null)
                    context.Serialize(item);
                else {
                    var type = item.GetType();
                    context.SerializeAs(type, item);
                }
            }
            context.Write('}');
        }
    }
    class JsonIDictionarySerializer<T, K, V> : IJsonTypeSerializer<T> where T : IDictionary<K,V> {
        public void Serialize(T value, JsonSerializationContext context) {
            context.Write('{');
            bool first = true;
            foreach (var item in value) {
                if (first)
                    first = false;
                else
                    context.Write(", ");
                context.Serialize(item.Key.ToString());
                context.Write(": ");
                context.Serialize<V>(item.Value);
            }
            context.Write('}');
        }
    }
}
