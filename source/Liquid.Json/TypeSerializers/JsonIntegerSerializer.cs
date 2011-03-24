using System;
using System.Globalization;

namespace Liquid.Json.TypeSerializers
{
    internal class JsonIntegerSerializer<T> : IJsonTypeSerializer<T>
        where T : IFormattable
    {
        private static readonly TryParseDelegate TryParse;

        static JsonIntegerSerializer()
        {
            TryParse = (TryParseDelegate) Delegate.CreateDelegate(typeof(TryParseDelegate), typeof(T), "TryParse");
        }

        #region IJsonTypeSerializer<T> Members

        public void Serialize(T @object, JsonSerializationContext context)
        {
            if (typeof(T) ==
                typeof(SByte))
                context.Writer.WriteValue((SByte) (object) @object, context.FormatProvider);
            else if (typeof(T) ==
                     typeof(Int16))
                context.Writer.WriteValue((Int16) (object) @object, context.FormatProvider);
            else if (typeof(T) ==
                     typeof(Int32))
                context.Writer.WriteValue((Int32) (object) @object, context.FormatProvider);
            else if (typeof(T) ==
                     typeof(Int64))
                context.Writer.WriteValue((Int64) (object) @object, context.FormatProvider);
            else if (typeof(T) ==
                     typeof(Byte))
                context.Writer.WriteValue((Byte) (object) @object, context.FormatProvider);
            else if (typeof(T) ==
                     typeof(UInt16))
                context.Writer.WriteValue((UInt16) (object) @object, context.FormatProvider);
            else if (typeof(T) ==
                     typeof(UInt32))
                context.Writer.WriteValue((UInt32) (object) @object, context.FormatProvider);
            else if (typeof(T) ==
                     typeof(UInt64))
                context.Writer.WriteValue((UInt64) (object) @object, context.FormatProvider);
            else throw new NotSupportedException();
        }

        public T Deserialize(JsonDeserializationContext context)
        {
            T result;
            bool success = TryParse(
                context.Reader.ReadNextAs(JsonTokenType.Integer),
                NumberStyles.Any,
                context.FormatProvider,
                out result
                );
            if (!success)
                throw new JsonDeserializationException();
            return result;
        }

        #endregion

        #region Nested type: TryParseDelegate

        private delegate bool TryParseDelegate(string str, NumberStyles styles, IFormatProvider format, out T result);

        #endregion
    }
}