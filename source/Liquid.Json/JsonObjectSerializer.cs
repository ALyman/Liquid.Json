using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Liquid.Json.TypeSerializers;

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

        public T Deserialize(JsonDeserializationContext context) {
            var value = Activator.CreateInstance<T>();
            var members = (from m in SelectMembers()
                           let f = m as FieldInfo
                           where f == null || !f.IsInitOnly
                           let p = m as PropertyInfo
                           where p == null || p.CanWrite
                           select m).ToLookup(m => m.Name.ToLower());
            context.Reader.ReadNextAs(JsonTokenType.ObjectStart);
            while (true) {
                context.Reader.ReadNext();
                if (context.Reader.Token == JsonTokenType.ObjectEnd)
                    break;
                else if (context.Reader.Token != JsonTokenType.String && context.Reader.Token != JsonTokenType.Identifier)
                    throw new JsonDeserializationException();
                var name = context.Reader.Text;
                if (name.StartsWith("\""))
                    name = Json.UnescapeString(name);
                MemberInfo selectedMember;
                var canidates = members[name.ToLower()];
                switch (canidates.Count()) {
                    case 0: throw new JsonDeserializationException();
                    case 1: selectedMember = canidates.Single(); break;
                    default:
                        if (canidates.Any(m => m.Name == name))
                            selectedMember = canidates.First(m => m.Name == name);
                        else
                            throw new JsonDeserializationException();
                        break;
                }
                context.Reader.ReadNextAs(JsonTokenType.Colon);
                DeserializeMember(value, selectedMember, context);
                context.Reader.ReadNext();
                if (context.Reader.Token == JsonTokenType.ObjectEnd)
                    break;
                else if (context.Reader.Token == JsonTokenType.Comma)
                    continue;
                else
                    throw new JsonDeserializationException();
            }
            if (context.Reader.Token != JsonTokenType.ObjectEnd)
                throw new JsonDeserializationException();
            return value;
        }

        protected virtual void DeserializeMember(T value, MemberInfo member, JsonDeserializationContext context) {
            Type memberType;
            switch (member.MemberType) {
                case MemberTypes.Field:
                    memberType = ((FieldInfo)member).FieldType;
                    break;
                case MemberTypes.Property:
                    memberType = ((PropertyInfo)member).PropertyType;
                    break;
                default: throw new NotSupportedException();
            }
            var memberValue = context.DeserializeAs(memberType);
            switch (member.MemberType) {
                case MemberTypes.Field:
                    ((FieldInfo)member).SetValue(value, memberValue);
                    break;
                case MemberTypes.Property:
                    ((PropertyInfo)member).SetValue(value, memberValue, null);
                    break;
                default: throw new NotSupportedException();
            }
        }
    }
}
