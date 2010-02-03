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
        static readonly MethodInfo DeserializeContextMethod;

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
            DeserializeContextMethod =
                (from m in typeof(JsonSerializer).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                 where m.Name == "Deserialize"
                 let p = m.GetParameters()
                 where p.Length == 1 && p[0].ParameterType == typeof(JsonDeserializationContext)
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

        public string Serialize<T>(T @object) {
            var writer = new StringWriter();
            Serialize(@object, writer);
            return writer.GetStringBuilder().ToString();
        }
        public void Serialize<T>(T @object, Stream stream) {
            using (var writer = new StreamWriter(stream)) {
                Serialize<T>(@object, writer);
            }
        }
        public void Serialize<T>(T @object, TextWriter writer) {
            Serialize<T>(@object, new JsonWriter(writer));
        }
        public void Serialize<T>(T @object, JsonWriter writer) {
            var context = new JsonSerializationContext(this, writer);
            Serialize(@object, context);
        }
        internal void Serialize<T>(T @object, JsonSerializationContext context) {
            if (@object == null) {
                context.Writer.WriteNull();
            } else {
                var s = GetSerializer<T>();
                s.Serialize(@object, context);
            }
        }

        public string SerializeAs(Type type, object @object) {
            var writer = new StringWriter();
            SerializeAs(type, @object, writer);
            return writer.GetStringBuilder().ToString();
        }
        public void SerializeAs(Type type, object @object, Stream stream) {
            using (var writer = new StreamWriter(stream)) {
                SerializeAs(type, @object, writer);
            }
        }
        public void SerializeAs(Type type, object @object, TextWriter writer) {
            var m = SerializeMethod
                .MakeGenericMethod(type);
            m.Invoke(this, new object[] { @object, writer });
        }
        internal void SerializeAs(Type type, object @object, JsonSerializationContext context) {
            var m = SerializeContextMethod
                .MakeGenericMethod(type);
            m.Invoke(this, new object[] { @object, context });
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

        public T Deserialize<T>(string str) {
            var reader = new StringReader(str);
            return Deserialize<T>(reader);
        }
        public T Deserialize<T>(Stream stream) {
            using (var reader = new StreamReader(stream)) {
                return Deserialize<T>(reader);
            }
        }
        public T Deserialize<T>(TextReader reader) {
            var jsonReader = new JsonReader(reader);
            return Deserialize<T>(jsonReader);
        }
        public T Deserialize<T>(JsonReader reader) {
            var context = new JsonDeserializationContext(this, reader);
            return Deserialize<T>(context);
        }
        internal T Deserialize<T>(JsonDeserializationContext context) {
            var s = GetSerializer<T>();
            return s.Deserialize(context);
        }

        internal object DeserializeAs(Type type, JsonDeserializationContext context) {
            var m = DeserializeContextMethod
                .MakeGenericMethod(type);
            return m.Invoke(this, new object[] { context });
        }

        public bool CanDeserializeInplace<T>() where T : class {
            var s = GetSerializer<T>();
            return s is IJsonTypeInplaceSerializer<T>;
        }

        internal bool CanDeserializeInplace(Type type) {
            throw new NotImplementedException();
        }
    }
}
