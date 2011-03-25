using System;

namespace Liquid.Json
{
    /// <summary>
    /// The context for deserialization
    /// </summary>
    public sealed class JsonDeserializationContext
    {
        private readonly JsonSerializer serailizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDeserializationContext"/> class.
        /// </summary>
        /// <param name="serailizer">The root serailizer.</param>
        /// <param name="reader">The reader.</param>
        public JsonDeserializationContext(JsonSerializer serailizer, JsonReader reader)
        {
            this.serailizer = serailizer;
            Reader = reader;
        }

        /// <summary>
        /// Gets or sets the JSON reader.
        /// </summary>
        /// <value>The JSON reader.</value>
        public JsonReader Reader { get; private set; }

        /// <summary>
        /// Gets or sets the format provider.
        /// </summary>
        /// <value>The format provider.</value>
        public IFormatProvider FormatProvider { get; set; }

        /// <summary>
        /// Deserializes the specified type.
        /// </summary>
        /// <typeparam name="T">The type to be deserialized</typeparam>
        /// <returns>The deserialized value</returns>
        public T Deserialize<T>()
        {
            return serailizer.Deserialize<T>(this);
        }

        /// <summary>
        /// Deserializes as the specified type.
        /// </summary>
        /// <param name="memberType">Type of the value to be deserialized.</param>
        /// <returns>The deserialized value</returns>
        public object DeserializeAs(Type memberType)
        {
            return serailizer.DeserializeAs(memberType, this);
        }

        /// <summary>
        /// Deserializes the value in-place.
        /// </summary>
        /// <typeparam name="T">The type of the value to be deserialized in-place</typeparam>
        /// <param name="value">The value to be deserailzed.</param>
        public void DeserializeInplace<T>(ref T value)
        {
            serailizer.DeserializeInplace(ref value, this);
        }

        /// <summary>
        /// Deserializes the value in-place.
        /// </summary>
        /// <param name="value">The value to be deserialized.</param>
        /// <param name="memberType">The type of the value to be deserialized in-place</param>
        public void DeserializeInplace(ref object value, Type memberType)
        {
            serailizer.DeserializeInplace(ref value, memberType, this);
        }

        /// <summary>
        /// Determines whether this instance can deserialize the specfied type in-place.
        /// </summary>
        /// <typeparam name="T">The type of the value to be deserialized in-place</typeparam>
        /// <returns>
        /// 	<c>true</c> if this instance can deserialize the specfied type in-place; otherwise, <c>false</c>.
        /// </returns>
        public bool CanDeserializeInplace<T>() where T : class
        {
            return serailizer.CanDeserializeInplace<T>();
        }

        /// <summary>
        /// Determines whether this instance can deserialize the specified type in-place.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance can deserialize the specfied type in-place; otherwise, <c>false</c>.
        /// </returns>
        public bool CanDeserializeInplace(Type type)
        {
            return serailizer.CanDeserializeInplace(type);
        }
    }
}