using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    class JsonIDictionarySerializer<T, K, V> : IJsonTypeInplaceSerializer<T> where T : IDictionary<K,V> {
        public void Serialize(T @object, JsonSerializationContext context) {
            context.Writer.WriteStartObject();
            foreach (var item in @object) {
                context.Writer.WriteName(item.Key.ToString());
                context.Serialize<V>(item.Value);
            }
            context.Writer.WriteEnd();
        }

        public T Deserialize(JsonDeserializationContext context) {
            if (typeof(T) == typeof(IDictionary<K,V>)) {
                T result = (T)(IDictionary<K, V>)new Dictionary<K, V>();
                DeserializeInto(ref result, context);
                return (T)(IDictionary<K, V>)result;
            } else {
                var result = Activator.CreateInstance<T>();
                DeserializeInto(ref result, context);
                return result;
            }
        }

        public void DeserializeInto(ref T @object, JsonDeserializationContext context) {
            context.Reader.ReadNextAs(JsonTokenType.ObjectStart);
            while (true) {
                context.Reader.ReadNext();
                if (context.Reader.Token == JsonTokenType.ObjectEnd)
                    break;
                else if (context.Reader.Token != JsonTokenType.String && context.Reader.Token != JsonTokenType.Identifier)
                    throw new JsonDeserializationException();
                var name = context.Reader.Text;
                if (name.StartsWith("\""))
                    name = Json.UnescapeString(name);
                context.Reader.ReadNextAs(JsonTokenType.Colon);
                var value = context.Deserialize<V>();
                @object.Add((K)(object)name, value);
                context.Reader.ReadNext();
                if (context.Reader.Token == JsonTokenType.ObjectEnd)
                    break;
                else if (context.Reader.Token == JsonTokenType.Comma)
                    continue;
                else
                    throw new JsonDeserializationException();
            }
            if (context.Reader.Token != JsonTokenType.ObjectEnd)
                throw new JsonDeserializationException();
        }
    }
}
