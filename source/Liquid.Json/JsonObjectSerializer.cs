using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Liquid.Json
{
    /// <summary>
    /// Serializes an object of the specified type
    /// </summary>
    /// <typeparam name="T">The tye of object being serialized.</typeparam>
    public class JsonObjectSerializer<T> : IJsonTypeInplaceSerializer<T>
    {
        private const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Public;

        #region IJsonTypeInplaceSerializer<T> Members

        /// <summary>
        /// Serializes the specified @object.
        /// </summary>
        /// <param name="object">The @object.</param>
        /// <param name="context">The context.</param>
        public void Serialize(T @object, JsonSerializationContext context)
        {
            context.Writer.WriteStartObject();
            bool first = true;
            foreach (MemberInfo member in SelectMembers()) {
                SerializeMember(@object, member, first, context);
                if (first)
                    first = false;
            }
            context.Writer.WriteEnd();
        }

        /// <summary>
        /// Deserializes the specified type.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The deserialzed value</returns>
        public T Deserialize(JsonDeserializationContext context)
        {
            var @object = Activator.CreateInstance<T>();
            DeserializeInto(ref @object, context);
            return @object;
        }

        /// <summary>
        /// Deserializes into the specified object.
        /// </summary>
        /// <param name="object">The object.</param>
        /// <param name="context">The context.</param>
        public void DeserializeInto(ref T @object, JsonDeserializationContext context)
        {
            ILookup<string, MemberInfo> members = (from m in SelectMembers()
                                                   let readOnly = m.IsReadOnly()
                                                   let type = m.GetMemberType()
                                                   where !readOnly || context.CanDeserializeInplace(type)
                                                   select m).ToLookup(m => m.Name.ToLower());
            context.Reader.ReadNextAs(JsonTokenType.ObjectStart);
            while (true) {
                context.Reader.ReadNext();
                if (context.Reader.Token ==
                    JsonTokenType.ObjectEnd)
                    break;
                else if (context.Reader.Token != JsonTokenType.String &&
                         context.Reader.Token != JsonTokenType.Identifier)
                    throw new JsonUnexpectedTokenException(context.Reader.Token, context.Reader.Text,
                                                           JsonTokenType.String, JsonTokenType.Identifier);
                string name = context.Reader.Text;
                if (name.StartsWith("\""))
                    name = Json.UnescapeString(name);
                MemberInfo selectedMember;
                IEnumerable<MemberInfo> canidates = members[name.ToLower()];
                switch (canidates.Count()) {
                    case 0:
                        throw new JsonDeserializationException();
                    case 1:
                        selectedMember = canidates.Single();
                        break;
                    default:
                        if (canidates.Any(m => m.Name == name))
                            selectedMember = canidates.First(m => m.Name == name);
                        else
                            throw new JsonDeserializationException(
                                string.Format(
                                    "Multiple candidate members for '{0}' could not be resolved by case sensitivity; candidates were: {1}",
                                    name,
                                    string.Join(", ",
                                                canidates.Select(m => string.Format("'{0}'", m.Name))
                                        )
                                    )
                                );
                        break;
                }
                context.Reader.ReadNextAs(JsonTokenType.Colon);
                DeserializeMember(@object, selectedMember, context);
                context.Reader.ReadNext();
                if (context.Reader.Token ==
                    JsonTokenType.ObjectEnd)
                    break;
                else if (context.Reader.Token ==
                         JsonTokenType.Comma)
                    continue;
                else
                    throw new JsonDeserializationException();
            }
            if (context.Reader.Token !=
                JsonTokenType.ObjectEnd)
                throw new JsonDeserializationException();
        }

        #endregion

        /// <summary>
        /// Serializes the member.
        /// </summary>
        /// <param name="object">The @object.</param>
        /// <param name="member">The member.</param>
        /// <param name="first">if set to <c>true</c> [first].</param>
        /// <param name="context">The context.</param>
        protected virtual void SerializeMember(T @object, MemberInfo member, bool first,
                                               JsonSerializationContext context)
        {
            SerializeName(@object, member, member.Name, context);
            SerializeValue(
                @object,
                member,
                member.GetMemberType(),
                member.GetMemberValue(@object),
                context
                );
        }

        /// <summary>
        /// Serializes the name.
        /// </summary>
        /// <param name="object">The @object.</param>
        /// <param name="member">The member.</param>
        /// <param name="memberName">Name of the member.</param>
        /// <param name="context">The context.</param>
        protected virtual void SerializeName(T @object, MemberInfo member, string memberName,
                                             JsonSerializationContext context)
        {
            context.Writer.WriteName(memberName);
        }

        /// <summary>
        /// Serializes the value.
        /// </summary>
        /// <param name="object">The @object.</param>
        /// <param name="member">The member.</param>
        /// <param name="memberType">Type of the member.</param>
        /// <param name="memberValue">The member value.</param>
        /// <param name="context">The context.</param>
        protected virtual void SerializeValue(T @object, MemberInfo member, Type memberType, object memberValue,
                                              JsonSerializationContext context)
        {
            context.SerializeAs(memberType, memberValue);
        }

        /// <summary>
        /// Selects the members.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<MemberInfo> SelectMembers()
        {
            return SelectProperties().Concat<MemberInfo>(SelectFields());
        }

        /// <summary>
        /// Selects the properties.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<PropertyInfo> SelectProperties()
        {
            return from property in typeof(T).GetProperties(FLAGS)
                   where !property.IsDefined(typeof(JsonIgnoreAttribute), true)
                   select property;
        }

        /// <summary>
        /// Selects the fields.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<FieldInfo> SelectFields()
        {
            return from field in typeof(T).GetFields(FLAGS)
                   where !field.IsDefined(typeof(JsonIgnoreAttribute), true)
                   select field;
        }

        /// <summary>
        /// Deserializes the member.
        /// </summary>
        /// <param name="object">The @object.</param>
        /// <param name="member">The member.</param>
        /// <param name="context">The context.</param>
        protected virtual void DeserializeMember(T @object, MemberInfo member, JsonDeserializationContext context)
        {
            Type memberType = member.GetMemberType();
            if (member.IsReadOnly()) {
                object value = member.GetMemberValue(@object);
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