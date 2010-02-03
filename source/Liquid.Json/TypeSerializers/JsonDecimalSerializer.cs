using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace Liquid.Json.TypeSerializers {
    class JsonDecimalSerializer : IJsonTypeSerializer<Decimal> {
        public void Serialize(decimal value, JsonSerializationContext context) {
            context.Write(value.ToString(context.FormatProvider));
        }

        public Decimal Deserialize(JsonDeserializationContext context) {
            Decimal result;
            bool success = Decimal.TryParse(
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
