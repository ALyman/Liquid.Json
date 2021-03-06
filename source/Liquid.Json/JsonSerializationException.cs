﻿using System;

namespace Liquid.Json
{
    /// <summary>
    /// An exception thrown by the JSON serialization process when an error occurs
    /// </summary>
    [Serializable]
    public sealed class JsonSerializationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSerializationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public JsonSerializationException(string message)
            : base(message) { }
    }
}