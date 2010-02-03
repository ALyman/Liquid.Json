using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace Liquid.Json.TypeSerializers {
    class JsonRealSerializer<T> : IJsonTypeSerializer<T>
    where T : IFormattable {

        delegate bool TryParseDelegate(string str, NumberStyles styles, IFormatProvider format, out T result);
        static readonly TryParseDelegate TryParse;

        static JsonRealSerializer() {
            TryParse = (TryParseDelegate)Delegate.CreateDelegate(typeof(TryParseDelegate), typeof(T), "TryParse");
        }

        public void Serialize(T @object, JsonSerializationContext context) {
            if (typeof(T) == typeof(Single))
                context.Writer.WriteValue((Single)(object)@object, context.FormatProvider);
            else if (typeof(T) == typeof(Double))
                context.Writer.WriteValue((Double)(object)@object, context.FormatProvider);
            else throw new NotSupportedException();
        }

        public T Deserialize(JsonDeserializationContext context) {
            T result;
            bool success = TryParse(
                context.Reader.ReadNextAs(JsonTokenType.Integer, JsonTokenType.Real),
                NumberStyles.Any, 
                context.FormatProvider, 
                out result
            );
            if (!success)
                throw new JsonDeserializationException();
            return result;
        }
    }
}
