using System;
using System.Collections.Generic;

namespace Liquid.Json
{
    /// <summary>
    /// The context for serialization
    /// </summary>
    public class JsonSerializationContext
    {
        private readonly HashSet<object> serialized = new HashSet<object>();
        private readonly JsonSerializer serializer;

        internal JsonSerializationContext(JsonSerializer serializer, JsonWriter writer)
        {
            this.serializer = serializer;
            Writer = writer;
        }

        /// <summary>
        /// Gets or sets the JSON writer.
        /// </summary>
        /// <value>The JSON writer.</value>
        public JsonWriter Writer { get; private set; }

        /// <summary>
        /// Gets the format provider.
        /// </summary>
        /// <value>The format provider.</value>
        public IFormatProvider FormatProvider { get { return serializer.FormatProvider; } }

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <typeparam name="T">The type of object to be serialized.</typeparam>
        /// <param name="object">The object to be serialized.</param>
        public void Serialize<T>(T @object)
        {
            serializer.Serialize(@object, this);
        }

        /// <summary>
        /// Serializes the specified object as the specified type.
        /// </summary>
        /// <param name="type">The type to be serialized.</param>
        /// <param name="object">The object serialized.</param>
        public void SerializeAs(Type type, object @object)
        {
            serializer.SerializeAs(type, @object, this);
        }

        /// <summary>
        /// Invoked before serializing the object.
        /// </summary>
        /// <typeparam name="T">The type of the object being deserialized</typeparam>
        /// <param name="object">The object being serialized.</param>
        protected internal void BeforeSerializing<T>(T @object)
        {
            if (!typeof(T).IsValueType &&
                !serialized.Add(@object))
                throw new JsonSerializationException("Cycle detected in object graph");
        }

        /// <summary>
        /// Invoked after serializing the object.
        /// </summary>
        /// <typeparam name="T">The type of the object that was deserialized</typeparam>
        /// <param name="object">The object that was serialized.</param>
        protected internal void AfterSerializing<T>(T @object)
        {
            serialized.Remove(@object);
        }
    }
}