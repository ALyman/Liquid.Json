using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    class JsonIListSerializer<T> : IJsonTypeSerializer<T>
        where T : IList {
        public void Serialize(T value, TextWriter writer, JsonSerializer serializer) {
            writer.Write('[');
            for (int i = 0; i < value.Count; i++) {
                if (i > 0)
                    writer.Write(", ");
                var item = value[i];
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
    class JsonIListSerializer<T, S> : IJsonTypeSerializer<T>
        where T : IList<S> {
        public void Serialize(T value, TextWriter writer, JsonSerializer serializer) {
            writer.Write('[');
            for (int i = 0; i < value.Count; i++) {
                if (i > 0)
                    writer.Write(", ");
                serializer.Serialize<S>(value[i], writer);
            }
            writer.Write(']');
        }
    }
}
