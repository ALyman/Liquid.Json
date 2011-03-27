using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Liquid.Json.TypeSerializers;

namespace Liquid.Json
{
    /// <summary>
    /// An object serializer factory, with a fuelt interface capable of specifiying which members to serialze/deserialize.
    /// </summary>
    /// <typeparam name="S"></typeparam>
    public class JsonObjectSerializerFactory<S> : IJsonTypeSerializerFactory
    {
        private readonly List<MemberInfo> members = new List<MemberInfo>();

        #region IJsonTypeSerializerFactory Members

        /// <summary>
        /// Creates a serializer for the specified type
        /// </summary>
        /// <typeparam name="T">The type to be serialized</typeparam>
        /// <param name="serializer">The root serializer</param>
        /// <returns>The type serializer</returns>
        public IJsonTypeSerializer<T> CreateSerializer<T>(JsonSerializer serializer)
        {
            if (typeof(T).IsAssignableFrom(typeof(S))) {
                return (IJsonTypeSerializer<T>)new Serializer(members);
            }
            return null;
        }

        #endregion

        /// <summary>
        /// The specified member is added to the list of members that will be serialized/deserialized.
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="specifier">The member to be serialized/deserialized.</param>
        /// <returns>The same instance of the factory.</returns>
        public JsonObjectSerializerFactory<S> WithMember<R>(Expression<Func<S, R>> specifier)
        {
            if (specifier == null) throw new ArgumentNullException("specifier");
            var memberExpr = specifier.Body as MemberExpression;
            if (memberExpr == null)
                throw new NotSupportedException("Can not serialize members that are not properties or fields");
            if (memberExpr.Expression != specifier.Parameters[0])
                throw new NotSupportedException("Can not serialize members that are not declared directly on the target object");
            members.Add(memberExpr.Member);

            return this;
        }

        #region Nested type: Serializer

        private class Serializer : JsonObjectSerializer<S>
        {
            private readonly IEnumerable<MemberInfo> members;

            public Serializer(IEnumerable<MemberInfo> members)
            {
                this.members = members;
            }

            protected override IEnumerable<MemberInfo> SelectMembers()
            {
                return members;
            }
        }

        #endregion
    }
}