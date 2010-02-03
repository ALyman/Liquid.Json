using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Liquid.Json.TypeSerializers;
using System.Collections;

namespace Liquid.Json.TypeSerializers {
    class DefaultJsonTypeSerializerFactory : IJsonTypeSerializerFactory {
        public IJsonTypeSerializer<T> CreateSerializer<T>(JsonSerializer serializer) {
            var typeCode = Type.GetTypeCode(typeof(T));
            switch (typeCode) {
                case TypeCode.DateTime:
                    return (IJsonTypeSerializer<T>)new JsonDateSerializer();
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return (IJsonTypeSerializer<T>)
                        Activator.CreateInstance(
                            typeof(JsonIntegerSerializer<>)
                                .MakeGenericType(typeof(T))
                        );
                case TypeCode.Single:
                case TypeCode.Double:
                    return (IJsonTypeSerializer<T>)
                        Activator.CreateInstance(
                            typeof(JsonRealSerializer<>)
                                .MakeGenericType(typeof(T))
                        );
                case TypeCode.Decimal:
                    return (IJsonTypeSerializer<T>)new JsonDecimalSerializer();
                case TypeCode.String:
                    return (IJsonTypeSerializer<T>)new JsonStringSerializer();
                case TypeCode.Object:
                    return TryMatch<T>(typeof(IDictionary<,>), typeof(JsonIDictionarySerializer<,,>))
                        ?? TryMatch<T>(typeof(IList<>), typeof(JsonIListSerializer<,>))
                        ?? TryMatch<T>(typeof(IEnumerable<>), typeof(JsonIEnumerableSerializer<,>))
                        ?? TryMatch<T>(typeof(IDictionary), typeof(JsonIDictionarySerializer<>))
                        ?? TryMatch<T>(typeof(IList), typeof(JsonIListSerializer<>))
                        ?? TryMatch<T>(typeof(IEnumerable), typeof(JsonIEnumerableSerializer<>))
                        ?? new JsonObjectSerializer<T>()
                        ;
                default:
                    throw new NotImplementedException();
            }
        }

        private IJsonTypeSerializer<T> TryMatch<T>(Type toMatch, Type resultType) {
            var q1 = (from iface in Enumerable.Repeat(typeof(T), 1)
                          .Concat(typeof(T).GetInterfaces())
                      where toMatch.IsGenericTypeDefinition == iface.IsGenericType
                      select iface).ToArray();
            var q2 = (from iface in q1
                      let def = iface.IsGenericType ? iface.GetGenericTypeDefinition() : iface
                      where def == toMatch
                      select new { iface, def }).ToArray();
            var q = from o in q2
                    select o.iface;
            var first = q.FirstOrDefault();
            if (first == null)
                return null;
            var @params = new List<Type>(first.GetGenericArguments());
            @params.Insert(0, typeof(T));

            return (IJsonTypeSerializer<T>)
                Activator.CreateInstance(
                    resultType.MakeGenericType(@params.ToArray())
                );
        }
    }
}
