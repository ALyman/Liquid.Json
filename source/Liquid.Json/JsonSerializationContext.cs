using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liquid.Json {
    public class JsonSerializationContext {
        JsonSerializer serializer;
        TextWriter writer;

        internal JsonSerializationContext(JsonSerializer serializer, TextWriter writer) {
            this.serializer = serializer;
            this.writer = writer;
        }

        public void Write(char ch) {
            writer.Write(ch);
        }
        public void Write(string str) {
            writer.Write(str);
        }

        public IFormatProvider FormatProvider { get { return serializer.FormatProvider; } }

        public void Serialize<T>(T value) {
            serializer.Serialize<T>(value, this);
        }
        public void SerializeAs(Type type, object value) {
            serializer.SerializeAs(type, value, this);
        }
    }
}
