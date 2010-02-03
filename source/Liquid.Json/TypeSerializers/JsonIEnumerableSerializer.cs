using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace Liquid.Json.TypeSerializers {
    class JsonIEnumerableSerializer<T> : IJsonTypeSerializer<T>
    where T : IEnumerable {
        public void Serialize(T value, TextWriter writer, JsonSerializer serializer) {
            writer.Write('[');
            bool first = true;
            foreach (var item in value) {
                if (first)
                    first = false;
                else
                    writer.Write(", ");
                if (item == null)
                    serializer.Serialize(item, writer);
                else {
                    var type = item.GetType();
                    serializer.SerializeAs(type, item, writer);
                }
            }
            writer.Write(']');
        }
    }
    class JsonIEnumerableSerializer<T, S> : IJsonTypeSerializer<T>
        where T : IEnumerable<S> {
        public void Serialize(T value, TextWriter writer, JsonSerializer serializer) {
            writer.Write('[');
            bool first = true;
            foreach (var item in value) {
                if (first)
                    first = false;
                else
                    writer.Write(", ");
                serializer.Serialize<S>(item, writer);
            }
            writer.Write(']');
        }
    }

}
