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

        static JsonSerializer() {
            SerializeMethod = 
                (from m in typeof(JsonSerializer).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                let p = m.GetParameters()
                where p.Length == 2 && p[1].ParameterType == typeof(TextWriter)
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
            IJsonTypeSerializer<T> s = GetSerializer<T>();
            s.Serialize(value, writer, this);
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

        public void SerializeAs(Type type, object item, TextWriter writer) {
            var m = SerializeMethod
                .MakeGenericMethod(type);

            m.Invoke(this, new object[] { item, writer });
        }
    }
}
