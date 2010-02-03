using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Liquid.Json.TypeSerializers {
    class JsonObjectSerializer<T> : IJsonTypeSerializer<T> {
        const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Public;

        public void Serialize(T value, TextWriter writer, JsonSerializer serializer) {
            writer.Write('{');
            bool first = true;
            foreach (var member in typeof(T).GetProperties(FLAGS)) {
                if (member.IsDefined(typeof(JsonIgnoreAttribute), true)) continue;
                if (first)
                    first = false;
                else
                    writer.Write(", ");
                serializer.Serialize(member.Name.ToString(), writer);
                writer.Write(": ");
                serializer.SerializeAs(member.PropertyType, member.GetValue(value, null), writer);
            }
            foreach (var member in typeof(T).GetFields(FLAGS)) {
                if (member.IsDefined(typeof(JsonIgnoreAttribute), true)) continue;
                if (first)
                    first = false;
                else
                    writer.Write(", ");
                serializer.Serialize(member.Name.ToString(), writer);
                writer.Write(": ");
                serializer.SerializeAs(member.FieldType, member.GetValue(value), writer);
            }
            writer.Write('}');
        }
    }
}
