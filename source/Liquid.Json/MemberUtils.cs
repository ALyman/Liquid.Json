using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Liquid.Json {
    static class MemberUtils {
        public static bool IsReadOnly(this MemberInfo member) {
            switch (member.MemberType) {
                case MemberTypes.Field:
                    return ((FieldInfo)member).IsInitOnly;
                case MemberTypes.Property:
                    return !((PropertyInfo)member).CanWrite;
                default: throw new NotSupportedException();
            }
        }

        public static Type GetMemberType(this MemberInfo member) {
            switch (member.MemberType) {
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                default: throw new NotSupportedException();
            }
        }
        public static object GetMemberValue(this MemberInfo member, object @object) {
            switch (member.MemberType) {
                case MemberTypes.Field:
                    return((FieldInfo)member).GetValue(@object);
                case MemberTypes.Property:
                    return ((PropertyInfo)member).GetValue(@object, null);
                default: throw new NotSupportedException();
            }
        }
        public static void SetMemberValue(this MemberInfo member, object @object, object value) {
            switch (member.MemberType) {
                case MemberTypes.Field:
                    ((FieldInfo)member).SetValue(@object, value);
                    break;
                case MemberTypes.Property:
                    ((PropertyInfo)member).SetValue(@object, value, null);
                    break;
                default: throw new NotSupportedException();
            }
        }
    }
}
