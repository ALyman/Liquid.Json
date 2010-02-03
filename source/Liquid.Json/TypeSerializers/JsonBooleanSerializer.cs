using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liquid.Json.TypeSerializers {
    class JsonBooleanSerializer : IJsonTypeSerializer<bool> {
        public void Serialize(bool value, JsonSerializationContext context) {
            if (value)
                context.Write("true");
            else
                context.Write("false");
        }
    }
}
