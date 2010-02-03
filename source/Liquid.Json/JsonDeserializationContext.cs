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
        public void DeserializeInplace<T>(T value) {
            throw new NotImplementedException();
        }
        public void DeserializeInplace(object value, Type memberType) {
            throw new NotImplementedException();
        }

        public bool CanDeserializeInplace<T>() where T : class {
            return serailizer.CanDeserializeInplace<T>();
        }
        public bool CanDeserializeInplace(Type type) {
            return serailizer.CanDeserializeInplace(type);
        }
    }
}
