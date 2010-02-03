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
    class JsonIListSerializer<T, S> : IJsonTypeSerializer<T>
        where T : IList<S> {
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
