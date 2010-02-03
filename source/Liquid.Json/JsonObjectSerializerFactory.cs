using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Liquid.Json {
    public class JsonObjectSerializerFactory<S> : IJsonTypeSerializerFactory {
        List<MemberInfo> members = new List<MemberInfo>();

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


        public JsonObjectSerializerFactory<S> WithMember<R>(Expression<Func<S, R>> expr) {
            var memberExpr = expr.Body as MemberExpression;
            if (memberExpr == null)
                throw new NotSupportedException();
            if (memberExpr.Expression != expr.Parameters[0])
                throw new NotSupportedException();
            members.Add(memberExpr.Member);

            return this;
        }
    }
}
