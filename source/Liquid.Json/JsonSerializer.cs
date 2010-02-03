﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq.Expressions;

namespace Liquid.Json {
    /// <summary>
    /// A JSON serialzier
    /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSerializer"/> class.
        /// </summary>
        /// <param name="factories">The factories.</param>
        public JsonSerializer(params IJsonTypeSerializerFactory[] factories)
            : this((IEnumerable<IJsonTypeSerializerFactory>)factories) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSerializer"/> class.
        /// </summary>
        /// <param name="factories">The factories.</param>
        public JsonSerializer(IEnumerable<IJsonTypeSerializerFactory> factories) {
            this.factories = new List<IJsonTypeSerializerFactory>(factories);

            this.factories.Add(new TypeSerializers.DefaultJsonTypeSerializerFactory());
        }

        /// <summary>
        /// Serializes the specified @object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object">The @object.</param>
        /// <returns></returns>
        public string Serialize<T>(T @object) {
            var writer = new StringWriter();
            Serialize(@object, writer);
            return writer.GetStringBuilder().ToString();
        }
        /// <summary>
        /// Serializes the specified @object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object">The @object.</param>
        /// <param name="stream">The stream.</param>
        public void Serialize<T>(T @object, Stream stream) {
            using (var writer = new StreamWriter(stream)) {
                Serialize<T>(@object, writer);
            }
        }
        /// <summary>
        /// Serializes the specified @object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object">The @object.</param>
        /// <param name="writer">The writer.</param>
        public void Serialize<T>(T @object, TextWriter writer) {
            Serialize<T>(@object, new JsonWriter(writer));
        }
        /// <summary>
        /// Serializes the specified @object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object">The @object.</param>
        /// <param name="writer">The writer.</param>
        public void Serialize<T>(T @object, JsonWriter writer) {
            var context = new JsonSerializationContext(this, writer);
            Serialize(@object, context);
        }
        internal void Serialize<T>(T @object, JsonSerializationContext context) {
            if (@object == null) {
                context.Writer.WriteNull();
            } else {
                context.BeforeSerializing(@object);
                var s = GetSerializer<T>();
                s.Serialize(@object, context);
                context.AfterSerializing(@object);
            }
        }

        delegate void SerializeDelegate(object @object, JsonSerializationContext context);

        /// <summary>
        /// Serializes as.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="object">The @object.</param>
        /// <returns></returns>
        public string SerializeAs(Type type, object @object) {
            var writer = new StringWriter();
            SerializeAs(type, @object, writer);
            return writer.GetStringBuilder().ToString();
        }
        /// <summary>
        /// Serializes as.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="object">The @object.</param>
        /// <param name="stream">The stream.</param>
        public void SerializeAs(Type type, object @object, Stream stream) {
            using (var writer = new StreamWriter(stream)) {
                SerializeAs(type, @object, writer);
            }
        }
        /// <summary>
        /// Serializes as.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="object">The @object.</param>
        /// <param name="writer">The writer.</param>
        public void SerializeAs(Type type, object @object, TextWriter writer) {
            var m = SerializeMethod
                .MakeGenericMethod(type);
            m.Invoke(this, new object[] { @object, writer });
        }
        internal void SerializeAs(Type type, object @object, JsonSerializationContext context) {
            var m = SerializeContextMethod
                .MakeGenericMethod(type);
            var objectParam = Expression.Parameter(typeof(object), "object");
            var contextParam = Expression.Parameter(typeof(JsonSerializationContext), "context");
            var lambda = Expression.Lambda<SerializeDelegate>(
                Expression.Call(
                    Expression.Constant(this),
                    m,
                    Expression.Convert(objectParam, type),
                    contextParam
                ),
                objectParam, contextParam
            );
            lambda.Compile()(@object, context);
        }

        /// <summary>
        /// Gets the serializer for the specified type.
        /// </summary>
        /// <typeparam name="T">The type to get a serializer for</typeparam>
        /// <returns>The serializer</returns>
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

        /// <summary>
        /// Gets or sets the format provider.
        /// </summary>
        /// <value>The format provider.</value>
        public IFormatProvider FormatProvider { get; set; }

        /// <summary>
        /// Deserializes the specified string.
        /// </summary>
        /// <typeparam name="T">The type to deserialize</typeparam>
        /// <param name="str">The string.</param>
        /// <returns>The deserialized value</returns>
        public T Deserialize<T>(string str) {
            var reader = new StringReader(str);
            return Deserialize<T>(reader);
        }
        /// <summary>
        /// Deserializes the specified stream.
        /// </summary>
        /// <typeparam name="T">The type to deserialize</typeparam>
        /// <param name="stream">The stream.</param>
        /// <returns>The deserialized value</returns>
        public T Deserialize<T>(Stream stream) {
            using (var reader = new StreamReader(stream)) {
                return Deserialize<T>(reader);
            }
        }
        /// <summary>
        /// Deserializes the specified reader.
        /// </summary>
        /// <typeparam name="T">The type to deserialize</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>The deserialized value</returns>
        public T Deserialize<T>(TextReader reader) {
            var jsonReader = new JsonReader(reader);
            return Deserialize<T>(jsonReader);
        }
        /// <summary>
        /// Deserializes the specified reader.
        /// </summary>
        /// <typeparam name="T">The type to deserialize</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>The deserialized value</returns>
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

        /// <summary>
        /// Determines whether this instance can deserialize the specfied type in-place.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        /// 	<c>true</c> if this instance can deserialize the specfied type in-place; otherwise, <c>false</c>.
        /// </returns>
        public bool CanDeserializeInplace<T>() where T : class {
            var s = GetSerializer<T>();
            return s is IJsonTypeInplaceSerializer<T>;
        }

        internal bool CanDeserializeInplace(Type type) {
            throw new NotImplementedException();
        }
    }
}
