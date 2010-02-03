using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    class JsonIListSerializer<T, S> : IJsonTypeInplaceSerializer<T>
        where T : IList<S> {
        public void Serialize(T @object, JsonSerializationContext context) {
            context.Writer.WriteStartArray();
            foreach (var item in @object) {
                context.Serialize<S>(item);
            }
            context.Writer.WriteEnd();
        }

        public T Deserialize(JsonDeserializationContext context) {
            if (typeof(T) == typeof(IList<S>)) {
                T result = (T)(IList<S>)new List<S>();
                DeserializeInto(ref result, context);
                return (T)(IList<S>)result;
            } else {
                var result = Activator.CreateInstance<T>();
                DeserializeInto(ref result, context);
                return result;
            }
        }

        public void DeserializeInto(ref T @object, JsonDeserializationContext context) {
            context.Reader.ReadNextAs(JsonTokenType.ArrayStart);
            while (true) {
                context.Reader.ReadNext();
                if (context.Reader.Token == JsonTokenType.ArrayEnd)
                    break;
                context.Reader.UndoRead();

                @object.Add(context.Deserialize<S>());

                context.Reader.ReadNext();
                if (context.Reader.Token == JsonTokenType.ArrayEnd)
                    break;
                else if (context.Reader.Token == JsonTokenType.Comma)
                    continue;
                else
                    throw new JsonDeserializationException();
            }
            if (context.Reader.Token != JsonTokenType.ArrayEnd)
                throw new JsonDeserializationException();
        }
    }
}
