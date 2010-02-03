using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    class JsonIDictionarySerializer<T> : IJsonTypeSerializer<T> where T : IDictionary {
        public void Serialize(T value, TextWriter writer, JsonSerializer serializer) {
            writer.Write('{');
            bool first = true;
            foreach (var key in value.Keys) {
                if (first)
                    first = false;
                else
                    writer.Write(", ");
                var item = value[key];
                serializer.Serialize(key.ToString(), writer);
                writer.Write(": ");
                if (item == null)
                    serializer.Serialize(item, writer);
                else {
                    var type = item.GetType();
                    serializer.SerializeAs(type, item, writer);
                }
            }
            writer.Write('}');
        }
    }
    class JsonIDictionarySerializer<T, K, V> : IJsonTypeSerializer<T> where T : IDictionary<K,V> {
        public void Serialize(T value, TextWriter writer, JsonSerializer serializer) {
            writer.Write('{');
            bool first = true;
            foreach (var item in value) {
                if (first)
                    first = false;
                else
                    writer.Write(", ");
                serializer.Serialize(item.Key.ToString(), writer);
                writer.Write(": ");
                serializer.Serialize<V>(item.Value, writer);
            }
            writer.Write('}');
        }
    }
}
