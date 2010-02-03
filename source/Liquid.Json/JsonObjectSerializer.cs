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

        public void Serialize(T @object, JsonSerializationContext context) {
            context.Writer.WriteStartObject();
            bool first = true;
            foreach (var member in SelectMembers()) {
                SerializeMember(@object, member, first, context);
                if (first)
                    first = false;
            }
            context.Writer.WriteEnd();
        }

        protected virtual void SerializeMember(T @object, MemberInfo member, bool first, JsonSerializationContext context) {
            SerializeName(@object, member, member.Name, context);
            SerializeValue(
                @object,
                member,
                member.GetMemberType(),
                member.GetMemberValue(@object),
                context
            );
        }
        protected virtual void SerializeName(T @object, MemberInfo member, string memberName, JsonSerializationContext context) {
            context.Writer.WriteName(memberName);
        }
        protected virtual void SerializeValue(T @object, MemberInfo member, Type memberType, object memberValue, JsonSerializationContext context) {
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
            var @object = Activator.CreateInstance<T>();
            var members = (from m in SelectMembers()
                           let readOnly = m.IsReadOnly()
                           let type = m.GetMemberType()
                           where !readOnly || context.CanDeserializeInplace(type)
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
                DeserializeMember(@object, selectedMember, context);
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
            return @object;
        }

        protected virtual void DeserializeMember(T @object, MemberInfo member, JsonDeserializationContext context) {
            Type memberType = member.GetMemberType();
            if (member.IsReadOnly()) {
                var value = member.GetMemberValue(@object);
                context.DeserializeInplace(value, memberType);
            } else {
                member.SetMemberValue(
                    @object, 
                    context.DeserializeAs(memberType)
                );
            }
        }
    }
}
