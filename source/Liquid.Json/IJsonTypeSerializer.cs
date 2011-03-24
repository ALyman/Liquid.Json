using System;

namespace Liquid.Json
{
    /// <summary>
    /// A JSON type serializer
    /// </summary>
    /// <typeparam name="T">The type that can be serialized/deserialzed</typeparam>
    public interface IJsonTypeSerializer<T>
    {
        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <param name="object">The object.</param>
        /// <param name="context">The context.</param>
        void Serialize(T @object, JsonSerializationContext context);

        /// <summary>
        /// Deserializes the specified type.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The deserialized value</returns>
        T Deserialize(JsonDeserializationContext context);
    }

    /// <summary>
    /// A JSON type serializer, capable of deserializing in-place.
    /// </summary>
    /// <typeparam name="T">The type that can be serialized/deserialzed in-place</typeparam>
    public interface IJsonTypeInplaceSerializer<T>
        : IJsonTypeSerializer<T>
    {
        /// <summary>
        /// Deserializes into the specified object.
        /// </summary>
        /// <param name="object">The object.</param>
        /// <param name="context">The context.</param>
        void DeserializeInto(ref T @object, JsonDeserializationContext context);
    }
}