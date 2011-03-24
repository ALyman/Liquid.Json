using System;
using System.Collections.Generic;

namespace Liquid.Json.TypeSerializers
{
    internal class JsonArraySerializer<T> : IJsonTypeSerializer<T[]>
    {
        #region IJsonTypeSerializer<T[]> Members

        public void Serialize(T[] @object, JsonSerializationContext context)
        {
            context.Writer.WriteStartArray();
            foreach (T item in @object) {
                context.Serialize(item);
            }
            context.Writer.WriteEnd();
        }

        public T[] Deserialize(JsonDeserializationContext context)
        {
            var resultList = new List<T>();

            context.Reader.ReadNextAs(JsonTokenType.ArrayStart);
            while (true) {
                context.Reader.ReadNext();
                if (context.Reader.Token ==
                    JsonTokenType.ArrayEnd)
                    break;
                context.Reader.UndoRead();

                resultList.Add(context.Deserialize<T>());

                context.Reader.ReadNext();
                if (context.Reader.Token ==
                    JsonTokenType.ArrayEnd)
                    break;
                else if (context.Reader.Token ==
                         JsonTokenType.Comma)
                    continue;
                else
                    throw new JsonDeserializationException();
            }
            if (context.Reader.Token !=
                JsonTokenType.ArrayEnd)
                throw new JsonDeserializationException();

            return resultList.ToArray();
        }

        #endregion
    }
}