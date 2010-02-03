using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Liquid.Json {
    public class JsonObjectSerializer<T> : IJsonTypeSerializer<T> {
        const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Public;

        public void Serialize(T value, JsonSerializationContext context) {
            context.Write('{');
            bool first = true;
            foreach (var member in SelectMembers()) {
                SerializeMember(value, member, first, context);
                if (first)
                    first = false;
            }
            context.Write('}');
        }

        protected virtual void SerializeMember(T value, MemberInfo member, bool first, JsonSerializationContext context) {
            if (!first)
                context.Write(", ");
            SerializeName(value, member, member.Name, context);
            context.Write(": ");
            Type memberType;
            object memberValue;

            switch (member.MemberType) {
                case MemberTypes.Field:
                    memberType = ((FieldInfo)member).FieldType;
                    memberValue = ((FieldInfo)member).GetValue(value);
                    break;
                case MemberTypes.Property:
                    memberType = ((PropertyInfo)member).PropertyType;
                    memberValue = ((PropertyInfo)member).GetValue(value, null);
                    break;
                default: throw new NotSupportedException();
            }
            SerializeValue(value, member, memberType, memberValue, context);
        }

        protected virtual void SerializeName(T value, MemberInfo member, string memberName, JsonSerializationContext context) {
            context.Serialize(memberName);
        }

        protected virtual void SerializeValue(T value, MemberInfo member, Type memberType, object memberValue, JsonSerializationContext context) {
            context.SerializeAs(memberType, memberValue);
        }

        protected virtual IEnumerable<MemberInfo> SelectMembers() {
            return SelectProperties().Concat<MemberInfo>(SelectFields());
        }
        protected virtual IEnumerable<PropertyInfo> SelectProperties() {
            return from property in typeof(T).GetProperties(FLAGS)
                   where !property.IsDefined(typeof(JsonIgnoreAttribute), true)
                   select property;
        }
        protected virtual IEnumerable<FieldInfo> SelectFields() {
            return from field in typeof(T).GetFields(FLAGS)
                   where !field.IsDefined(typeof(JsonIgnoreAttribute), true)
                   select field;
        }
    }
}
