using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liquid.Json.TypeSerializers {
    class JsonBooleanSerializer : IJsonTypeSerializer<bool> {

        public void Serialize(bool value, System.IO.TextWriter writer, JsonSerializer serializer) {
            if (value)
                writer.Write("true");
            else
                writer.Write("false");
        }
    }
}
