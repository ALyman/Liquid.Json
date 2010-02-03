using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liquid.Json {
    /// <summary>
    /// An exception thrown by the JSON deserialization process when an error occurs 
    /// </summary>
    public class JsonDeserializationException : Exception {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDeserializationException"/> class.
        /// </summary>
        public JsonDeserializationException() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDeserializationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public JsonDeserializationException(string message) : base(message) { }
    }
}
