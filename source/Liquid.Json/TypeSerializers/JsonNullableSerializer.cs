using System;

namespace Liquid.Json.TypeSerializers
{
    internal class JsonNullableSerializer<T, S> : IJsonTypeSerializer<T>
        where S : struct
    {
        #region IJsonTypeSerializer<T> Members

        public void Serialize(T @object, JsonSerializationContext context)
        {
            var n = (S?) (object) @object;
            if (n.HasValue)
                context.Serialize(n.Value);
            else
                context.Writer.WriteNull();
        }


        public T Deserialize(JsonDeserializationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}