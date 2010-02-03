using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Liquid.Json {
    public class JsonSerializer {
        static readonly MethodInfo SerializeMethod;
        static readonly MethodInfo SerializeContextMethod;

        static JsonSerializer() {
            SerializeMethod =
                (from m in typeof(JsonSerializer).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                 where m.Name == "Serialize"
                 let p = m.GetParameters()
                 where p.Length == 2 && p[1].ParameterType == typeof(TextWriter)
                 select m).Single();
            SerializeContextMethod =
                (from m in typeof(JsonSerializer).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                 where m.Name == "Serialize"
                 let p = m.GetParameters()
                 where p.Length == 2 && p[1].ParameterType == typeof(JsonSerializationContext)
                 select m).Single();
        }

        List<IJsonTypeSerializerFactory> factories;

        Dictionary<Type, object> serializers = new Dictionary<Type, object>();

        public JsonSerializer(params IJsonTypeSerializerFactory[] factories)
            : this((IEnumerable<IJsonTypeSerializerFactory>)factories) {
        }

        public JsonSerializer(IEnumerable<IJsonTypeSerializerFactory> factories) {
            this.factories = new List<IJsonTypeSerializerFactory>(factories);

            this.factories.Add(new TypeSerializers.DefaultJsonTypeSerializerFactory());
        }

        public string Serialize<T>(T value) {
            var writer = new StringWriter();
            Serialize(value, writer);
            return writer.GetStringBuilder().ToString();
        }
        public void Serialize<T>(T value, Stream stream) {
            using (var writer = new StreamWriter(stream)) {
                Serialize<T>(value, writer);
            }
        }
        public void Serialize<T>(T value, TextWriter writer) {
            var context = new JsonSerializationContext(this, writer);
            Serialize(value, context);
        }
        internal void Serialize<T>(T value, JsonSerializationContext context) {
            if (value == null) {
                context.Write("null");
            } else {
                var s = GetSerializer<T>();
                s.Serialize(value, context);
            }
        }

        public string SerializeAs(Type type, object value) {
            var writer = new StringWriter();
            SerializeAs(type, value, writer);
            return writer.GetStringBuilder().ToString();
        }
        public void SerializeAs(Type type, object value, Stream stream) {
            using (var writer = new StreamWriter(stream)) {
                SerializeAs(type, value, writer);
            }
        }
        public void SerializeAs(Type type, object value, TextWriter writer) {
            var m = SerializeMethod
                .MakeGenericMethod(type);
            m.Invoke(this, new object[] { value, writer });
        }
        internal void SerializeAs(Type type, object value, JsonSerializationContext context) {
            var m = SerializeContextMethod
                .MakeGenericMethod(type);
            m.Invoke(this, new object[] { value, context });
        }

        protected IJsonTypeSerializer<T> GetSerializer<T>() {
            object result;
            if (!serializers.TryGetValue(typeof(T), out result)) {
                var q = from f in factories
                        let s = f.CreateSerializer<T>(this)
                        where s != null
                        select s;
                result = q.First();
                serializers.Add(typeof(T), result);
            }
            return (IJsonTypeSerializer<T>)result;
        }

        public IFormatProvider FormatProvider { get; set; }
    }
}
