using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Liquid.Json.TypeSerializers;

namespace Liquid.Json
{
    /// <summary>
    /// A JSON serialzier
    /// </summary>
    public class JsonSerializer
    {
        private static readonly MethodInfo SerializeMethod;
        private static readonly MethodInfo SerializeContextMethod;
        private static readonly MethodInfo DeserializeContextMethod;
        private static readonly MethodInfo DeserializeInplaceContextMethod;
        private static readonly MethodInfo CanDeserializeInplaceContextMethod;

        private readonly List<IJsonTypeSerializerFactory> factories;

        private readonly Dictionary<Type, object> serializers = new Dictionary<Type, object>();

        static JsonSerializer()
        {
            SerializeMethod =
                (from m in typeof(JsonSerializer).GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                 where m.Name == "Serialize"
                 let p = m.GetParameters()
                 where p.Length == 2 && p[1].ParameterType == typeof(TextWriter)
                 select m).Single();
            SerializeContextMethod =
                (from m in typeof(JsonSerializer).GetMethods(BindingFlags.NonPublic | BindingFlags.NonPublic | BindingFlags.Instance)
                 where m.Name == "Serialize"
                 let p = m.GetParameters()
                 where p.Length == 2 && p[1].ParameterType == typeof(JsonSerializationContext)
                 select m).Single();
            DeserializeContextMethod =
                (from m in typeof(JsonSerializer).GetMethods(BindingFlags.NonPublic | BindingFlags.NonPublic | BindingFlags.Instance)
                 where m.Name == "Deserialize"
                 let p = m.GetParameters()
                 where p.Length == 1 && p[0].ParameterType == typeof(JsonDeserializationContext)
                 select m).Single();
            DeserializeInplaceContextMethod =
                (from m in typeof(JsonSerializer).GetMethods(BindingFlags.NonPublic | BindingFlags.NonPublic | BindingFlags.Instance)
                 where m.Name == "DeserializeInplace"
                 let p = m.GetParameters()
                 where p.Length == 2 && p[1].ParameterType == typeof(JsonDeserializationContext)
                 select m).Single();
            CanDeserializeInplaceContextMethod =
                (from m in typeof(JsonSerializer).GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                 where m.Name == "CanDeserializeInplace"
                 let p = m.GetParameters()
                 where p.Length == 0
                 select m).Single();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSerializer"/> class.
        /// </summary>
        /// <param name="factories">The factories.</param>
        public JsonSerializer(params IJsonTypeSerializerFactory[] factories)
            : this((IEnumerable<IJsonTypeSerializerFactory>)factories) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSerializer"/> class.
        /// </summary>
        /// <param name="factories">The factories.</param>
        public JsonSerializer(IEnumerable<IJsonTypeSerializerFactory> factories)
        {
            this.factories = new List<IJsonTypeSerializerFactory>(factories);

            this.factories.Add(new DefaultJsonTypeSerializerFactory());
        }

        /// <summary>
        /// Gets or sets the format provider.
        /// </summary>
        /// <value>The format provider.</value>
        public IFormatProvider FormatProvider { get; set; }

        /// <summary>
        /// Serializes the specified @object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object">The @object.</param>
        /// <returns></returns>
        public string Serialize<T>(T @object)
        {
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
        public void Serialize<T>(T @object, Stream stream)
        {
            using (var writer = new StreamWriter(stream)) {
                Serialize(@object, writer);
            }
        }

        /// <summary>
        /// Serializes the specified @object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object">The @object.</param>
        /// <param name="writer">The writer.</param>
        public void Serialize<T>(T @object, TextWriter writer)
        {
            Serialize(@object, new JsonWriter(writer));
        }

        /// <summary>
        /// Serializes the specified @object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object">The @object.</param>
        /// <param name="writer">The writer.</param>
        public void Serialize<T>(T @object, JsonWriter writer)
        {
            var context = new JsonSerializationContext(this, writer);
            Serialize(@object, context);
        }

        internal void Serialize<T>(T @object, JsonSerializationContext context)
        {
            IJsonTypeSerializer<T> s = GetSerializer<T>();
            if (@object == null && !(s is IWantNullValues)) {
                context.Writer.WriteNull();
            } else {
                context.BeforeSerializing(@object);
                s.Serialize(@object, context);
                context.AfterSerializing(@object);
            }
        }

        /// <summary>
        /// Serializes as.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="object">The @object.</param>
        /// <returns></returns>
        public string SerializeAs(Type type, object @object)
        {
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
        public void SerializeAs(Type type, object @object, Stream stream)
        {
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
        public void SerializeAs(Type type, object @object, TextWriter writer)
        {
            MethodInfo m = SerializeMethod
                .MakeGenericMethod(type);
            // TODO: Remove the MethodInfo.Invoke
            try {
                // TODO: Replace this MethodInfo.Invoke with an expression tree
                m.Invoke(this, new[] { @object, writer });
#if DEBUG
            } catch (TargetInvocationException ex) {
                throw ex.InnerException;
#endif
            } finally { }
        }

        internal void SerializeAs(Type type, object @object, JsonSerializationContext context)
        {
            MethodInfo m = SerializeContextMethod
                .MakeGenericMethod(type);
            ParameterExpression objectParam = Expression.Parameter(typeof(object), "object");
            ParameterExpression contextParam = Expression.Parameter(typeof(JsonSerializationContext), "context");
            Expression<SerializeDelegate> lambda = Expression.Lambda<SerializeDelegate>(
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
        protected IJsonTypeSerializer<T> GetSerializer<T>()
        {
            object result;
            if (!serializers.TryGetValue(typeof(T), out result)) {
                IEnumerable<IJsonTypeSerializer<T>> q = from f in factories
                                                        let s = f.CreateSerializer<T>(this)
                                                        where s != null
                                                        select s;
                result = q.First();
                serializers.Add(typeof(T), result);
            }
            return (IJsonTypeSerializer<T>)result;
        }

        /// <summary>
        /// Deserializes the specified string.
        /// </summary>
        /// <typeparam name="T">The type to deserialize</typeparam>
        /// <param name="str">The string.</param>
        /// <returns>The deserialized value</returns>
        public T Deserialize<T>(string str)
        {
            var reader = new StringReader(str);
            return Deserialize<T>(reader);
        }

        /// <summary>
        /// Deserializes the specified stream.
        /// </summary>
        /// <typeparam name="T">The type to deserialize</typeparam>
        /// <param name="stream">The stream.</param>
        /// <returns>The deserialized value</returns>
        public T Deserialize<T>(Stream stream)
        {
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
        public T Deserialize<T>(TextReader reader)
        {
            var jsonReader = new JsonReader(reader);
            return Deserialize<T>(jsonReader);
        }

        /// <summary>
        /// Deserializes the specified reader.
        /// </summary>
        /// <typeparam name="T">The type to deserialize</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>The deserialized value</returns>
        public T Deserialize<T>(JsonReader reader)
        {
            var context = new JsonDeserializationContext(this, reader);
            return Deserialize<T>(context);
        }

        internal T Deserialize<T>(JsonDeserializationContext context)
        {
            IJsonTypeSerializer<T> s = GetSerializer<T>();
            return s.Deserialize(context);
        }

        internal object DeserializeAs(Type type, JsonDeserializationContext context)
        {
            MethodInfo m = DeserializeContextMethod.MakeGenericMethod(type);
            try {
                // TODO: Replace this MethodInfo.Invoke with an expression tree
                return m.Invoke(this, new[] { context });
#if DEBUG
            } catch (TargetInvocationException ex) {
                throw ex.InnerException;
#endif
            } finally { }
        }

        /// <summary>
        /// Deserializes the specified string.
        /// </summary>
        /// <typeparam name="T">The type to deserialize</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="str">The string.</param>
        public void DeserializeInto<T>(ref T target, string str)
        {
            var reader = new StringReader(str);
            DeserializeInto<T>(ref target, reader);
        }

        /// <summary>
        /// Deserializes the specified stream.
        /// </summary>
        /// <typeparam name="T">The type to deserialize</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="stream">The stream.</param>
        public void DeserializeInto<T>(ref T target, Stream stream)
        {
            using (var reader = new StreamReader(stream)) {
                DeserializeInto<T>(ref target, reader);
            }
        }

        /// <summary>
        /// Deserializes the specified reader.
        /// </summary>
        /// <typeparam name="T">The type to deserialize</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="reader">The reader.</param>
        public void DeserializeInto<T>(ref T target, TextReader reader)
        {
            var jsonReader = new JsonReader(reader);
            DeserializeInto<T>(ref target, jsonReader);
        }

        /// <summary>
        /// Deserializes the specified reader.
        /// </summary>
        /// <typeparam name="T">The type to deserialize</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="reader">The reader.</param>
        public void DeserializeInto<T>(ref T target, JsonReader reader)
        {
            var context = new JsonDeserializationContext(this, reader);
            DeserializeInplace<T>(ref target, context);
        }

        /// <summary>
        /// Determines whether this instance can deserialize the specified type in-place.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        /// 	<c>true</c> if this instance can deserialize the specified type in-place; otherwise, <c>false</c>.
        /// </returns>
        public bool CanDeserializeInplace<T>() where T : class
        {
            var s = GetSerializer<T>();
            return s is IJsonTypeInplaceSerializer<T>;
        }

        internal bool CanDeserializeInplace(Type type)
        {
            var m = CanDeserializeInplaceContextMethod.MakeGenericMethod(type);
            var d = (Func<bool>)Delegate.CreateDelegate(
                typeof(Func<bool>),
                this,
                m);
            return d();
        }

        #region Nested type: SerializeDelegate

        private delegate void SerializeDelegate(object @object, JsonSerializationContext context);

        #endregion

        internal void DeserializeInplace<T>(ref T value, JsonDeserializationContext context)
        {
            if (value == null)
                throw new ArgumentNullException("@object");

            var s = GetSerializer<T>() as IJsonTypeInplaceSerializer<T>;
            s.DeserializeInto(ref value, context);
        }

        internal void DeserializeInplace(ref object value, Type type, JsonDeserializationContext context)
        {
            if (value == null)
                throw new ArgumentNullException("@object");

            var m = DeserializeInplaceContextMethod.MakeGenericMethod(type);
            try {
                // TODO: Replace this MethodInfo.Invoke with an expression tree
                var args = new[] { value, context };
                m.Invoke(this, args);
                value = args[0];
#if DEBUG
            } catch (TargetInvocationException ex) {
                throw ex.InnerException;
#endif
            } finally { }
        }
    }
}
