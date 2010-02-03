using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    class JsonIListSerializer<T> : IJsonTypeSerializer<T>
        where T : IList {
        public void Serialize(T value, JsonSerializationContext context) {
            context.Write('[');
            for (int i = 0; i < value.Count; i++) {
                if (i > 0)
                    context.Write(", ");
                var item = value[i];
                if (item == null)
                    context.Serialize(item);
                else {
                    var type = item.GetType();
                    context.SerializeAs(type, item);
                }
            }
            context.Write(']');
        }
    }
    class JsonIListSerializer<T, S> : IJsonTypeSerializer<T>
        where T : IList<S> {
        public void Serialize(T value, JsonSerializationContext context) {
            context.Write('[');
            for (int i = 0; i < value.Count; i++) {
                if (i > 0)
                    context.Write(", ");
                context.Serialize<S>(value[i]);
            }
            context.Write(']');
        }
    }
}
