using System;
using System.Collections.Generic;

namespace Liquid.Json.TypeSerializers
{
    internal class JsonIEnumerableSerializer<T, S> : IJsonTypeSerializer<T>
        where T : IEnumerable<S>
    {
        #region IJsonTypeSerializer<T> Members

        public void Serialize(T @object, JsonSerializationContext context)
        {
            context.Writer.WriteStartArray();
            foreach (S item in @object) {
                context.Serialize(item);
            }
            context.Writer.WriteEnd();
        }


        public T Deserialize(JsonDeserializationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}