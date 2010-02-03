using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Liquid.Json.TypeSerializers {
    class JsonObjectSerializer<T> : IJsonTypeSerializer<T> {
        const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Public;

        public void Serialize(T value, JsonSerializationContext context) {
            context.Write('{');
            bool first = true;
            foreach (var member in typeof(T).GetProperties(FLAGS)) {
                if (member.IsDefined(typeof(JsonIgnoreAttribute), true)) continue;
                if (first)
                    first = false;
                else
                    context.Write(", ");
                context.Serialize(member.Name.ToString());
                context.Write(": ");
                context.SerializeAs(member.PropertyType, member.GetValue(value, null));
            }
            foreach (var member in typeof(T).GetFields(FLAGS)) {
                if (member.IsDefined(typeof(JsonIgnoreAttribute), true)) continue;
                if (first)
                    first = false;
                else
                    context.Write(", ");
                context.Serialize(member.Name.ToString());
                context.Write(": ");
                context.SerializeAs(member.FieldType, member.GetValue(value));
            }
            context.Write('}');
        }
    }
}
