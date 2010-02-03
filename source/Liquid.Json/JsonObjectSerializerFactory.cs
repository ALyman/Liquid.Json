using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Liquid.Json {
    /// <summary>
    /// An object serializer factory, with a fuelt interface capable of specifiying which members to serialze/deserialize.
    /// </summary>
    /// <typeparam name="S"></typeparam>
    public class JsonObjectSerializerFactory<S> : IJsonTypeSerializerFactory {
        List<MemberInfo> members = new List<MemberInfo>();

        /// <summary>
        /// Creates a serializer for the specified type
        /// </summary>
        /// <typeparam name="T">The type to be serialized</typeparam>
        /// <param name="serializer">The root serializer</param>
        /// <returns>The type serializer</returns>
        public IJsonTypeSerializer<T> CreateSerializer<T>(JsonSerializer serializer) {
            if (typeof(T).IsAssignableFrom(typeof(S))) {
                return (IJsonTypeSerializer<T>)new Serializer(members);
            }
            return null;
        }

        class Serializer : JsonObjectSerializer<S> {
            IEnumerable<MemberInfo> members;

            public Serializer(IEnumerable<MemberInfo> members) {
                this.members = members;
            }

            protected override IEnumerable<MemberInfo> SelectMembers() {
                return this.members;
            }
        }


        /// <summary>
        /// The specified member is added to the list of members that will be serialzed/deserialzed.
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="specifier">The member to be serialzed/deserialzed.</param>
        /// <returns>The same instance of the factory.</returns>
        public JsonObjectSerializerFactory<S> WithMember<R>(Expression<Func<S, R>> specifier) {
            var memberExpr = specifier.Body as MemberExpression;
            if (memberExpr == null)
                throw new NotSupportedException();
            if (memberExpr.Expression != specifier.Parameters[0])
                throw new NotSupportedException();
            members.Add(memberExpr.Member);

            return this;
        }
    }
}
