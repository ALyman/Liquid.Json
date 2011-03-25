using System;
using System.Diagnostics;
using System.Globalization;

namespace Liquid.Json.TypeSerializers
{
    internal class JsonDecimalSerializer : IJsonTypeSerializer<Decimal>
    {
        #region IJsonTypeSerializer<decimal> Members

        public void Serialize(decimal @object, JsonSerializationContext context)
        {
            context.Writer.WriteValue(@object, context.FormatProvider);
        }

        public Decimal Deserialize(JsonDeserializationContext context)
        {
            Decimal result;
            bool success = Decimal.TryParse(
                context.Reader.ReadNextAs(JsonTokenType.Integer, JsonTokenType.Real),
                NumberStyles.Any,
                context.FormatProvider,
                out result
                );
            Debug.Assert(success);
            return result;
        }

        #endregion
    }
}