using System;

namespace Liquid.Json.TypeSerializers
{
    internal class JsonNullableSerializer<T, S> : IJsonTypeSerializer<T>, IWantNullValues
        where S : struct
    {
        #region IJsonTypeSerializer<T> Members

        public void Serialize(T @object, JsonSerializationContext context)
        {
            var n = (S?)(object)@object;
            if (n.HasValue)
                context.Serialize(n.Value);
            else
                context.Writer.WriteNull();
        }

        public T Deserialize(JsonDeserializationContext context)
        {
            if (!context.Reader.ReadNext())
                throw context.Reader.UnexpectedTokenException(JsonTokenType.Identifier, JsonTokenType.Any);

            if (context.Reader.Token == JsonTokenType.Identifier && context.Reader.Text == "null") {
                return (T)(object)(S?)null;
            } else {
                context.Reader.UndoRead();
                return (T)(object)(S?)context.Deserialize<S>();
            }
        }

        #endregion
    }
}