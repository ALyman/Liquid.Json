using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liquid.Json {
    public class JsonSerializationContext {
        JsonSerializer serializer;

        internal JsonSerializationContext(JsonSerializer serializer, JsonWriter writer) {
            this.serializer = serializer;
            this.Writer = writer;
        }

        public JsonWriter Writer { get; private set; }

        public IFormatProvider FormatProvider { get { return serializer.FormatProvider; } }

        public void Serialize<T>(T @object) {
            serializer.Serialize<T>(@object, this);
        }
        public void SerializeAs(Type type, object @object) {
            serializer.SerializeAs(type, @object, this);
        }
    }
}
