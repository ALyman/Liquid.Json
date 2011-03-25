using System;
using System.Diagnostics;
using System.Globalization;

namespace Liquid.Json.TypeSerializers
{
    internal class JsonRealSerializer<T> : IJsonTypeSerializer<T>
        where T : IFormattable
    {
        private static readonly TryParseDelegate TryParse;

        static JsonRealSerializer()
        {
            TryParse = (TryParseDelegate)Delegate.CreateDelegate(typeof(TryParseDelegate), typeof(T), "TryParse");
        }

        #region IJsonTypeSerializer<T> Members

        public void Serialize(T @object, JsonSerializationContext context)
        {
            if (typeof(T) ==
                typeof(Single))
                context.Writer.WriteValue((Single)(object)@object, context.FormatProvider);
            else if (typeof(T) ==
                     typeof(Double))
                context.Writer.WriteValue((Double)(object)@object, context.FormatProvider);
            else throw new NotSupportedException();
        }

        public T Deserialize(JsonDeserializationContext context)
        {
            T result;
            bool success = TryParse(
                context.Reader.ReadNextAs(JsonTokenType.Integer, JsonTokenType.Real),
                NumberStyles.Any,
                context.FormatProvider,
                out result
                );
            Debug.Assert(success);
            return result;
        }

        #endregion

        #region Nested type: TryParseDelegate

        private delegate bool TryParseDelegate(string str, NumberStyles styles, IFormatProvider format, out T result);

        #endregion
    }
}