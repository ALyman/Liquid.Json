using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    public class JsonDateSerializer : IJsonTypeSerializer<DateTime> {
        static readonly DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public void Serialize(DateTime value, JsonSerializationContext context) {
            var span = value - EPOCH;
            context.Writer.WriteLiteralValue(string.Format("new Date({0})", (long)span.TotalSeconds));
        }

        public DateTime Deserialize(JsonDeserializationContext context) {
            throw new NotImplementedException();
        }
    }
}
