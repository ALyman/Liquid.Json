using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    class JsonDateSerializer : IJsonTypeSerializer<DateTime> {
        static readonly DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public void Serialize(DateTime @object, JsonSerializationContext context) {
            var span = @object - EPOCH;
            context.Writer.WriteStartConstructor("Date");
            context.Writer.WriteValue((long)span.TotalSeconds);
            context.Writer.WriteEnd();
        }

        public DateTime Deserialize(JsonDeserializationContext context) {
            throw new NotImplementedException();
        }
    }
}
