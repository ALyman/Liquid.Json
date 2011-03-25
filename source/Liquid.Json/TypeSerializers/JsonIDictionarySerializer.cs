using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Liquid.Json.TypeSerializers
{
    internal class JsonIDictionarySerializer<T, K, V> : IJsonTypeInplaceSerializer<T>
        where T : IDictionary<K, V>
    {
        #region IJsonTypeInplaceSerializer<T> Members

        public void Serialize(T @object, JsonSerializationContext context)
        {
            context.Writer.WriteStartObject();
            foreach (var item in @object) {
                context.Writer.WriteName(item.Key.ToString());
                context.Serialize(item.Value);
            }
            context.Writer.WriteEnd();
        }

        public T Deserialize(JsonDeserializationContext context)
        {
            if (typeof(T) ==
                typeof(IDictionary<K, V>)) {
                var result = (T)(IDictionary<K, V>)new Dictionary<K, V>();
                DeserializeInto(ref result, context);
                return result;
            } else {
                var result = Activator.CreateInstance<T>();
                DeserializeInto(ref result, context);
                return result;
            }
        }

        public void DeserializeInto(ref T @object, JsonDeserializationContext context)
        {
            context.Reader.ReadNextAs(JsonTokenType.ObjectStart);
            while (true) {
                context.Reader.ReadNext();
                if (context.Reader.Token == JsonTokenType.ObjectEnd)
                    break;
                else if (context.Reader.Token != JsonTokenType.String && context.Reader.Token != JsonTokenType.Identifier)
                    throw context.Reader.UnexpectedTokenException(JsonTokenType.String, JsonTokenType.Identifier);
                string name = context.Reader.Text;
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
                    throw context.Reader.UnexpectedTokenException(JsonTokenType.Comma, JsonTokenType.ObjectEnd);
            }
            Debug.Assert(context.Reader.Token == JsonTokenType.ObjectEnd);
        }

        #endregion
    }
}