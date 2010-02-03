using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liquid.Json {
    public sealed class JsonDeserializationContext {
        private JsonSerializer serailizer;

        public JsonDeserializationContext(JsonSerializer serailizer, JsonReader reader) {
            this.serailizer = serailizer;
            this.Reader = reader;
        }

        public JsonReader Reader { get; private set; }

        public IFormatProvider FormatProvider { get; set; }

        public T Deserialize<T>() {
            return serailizer.Deserialize<T>(this);
        }
        public object DeserializeAs(Type memberType) {
            return serailizer.DeserializeAs(memberType, this);
        }
    }
}
