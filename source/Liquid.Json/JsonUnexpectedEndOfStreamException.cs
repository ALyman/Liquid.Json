using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liquid.Json
{
    /// <summary>
    /// An exception thrown when we unexpectedly got to the end of the stream
    /// </summary>
    public class JsonUnexpectedEndOfStreamException : JsonDeserializationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonUnexpectedEndOfStreamException"/> class.
        /// </summary>
        /// <param name="expectedTypes">The expected types.</param>
        public JsonUnexpectedEndOfStreamException(params JsonTokenType[] expectedTypes)
            : this(expectedTypes.AsEnumerable())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonUnexpectedEndOfStreamException"/> class.
        /// </summary>
        /// <param name="expectedTypes">The expected types.</param>
        public JsonUnexpectedEndOfStreamException(IEnumerable<JsonTokenType> expectedTypes)
            : base(string.Format("Unexpected end of file{0}", expectedTypes.ToErrorString()))
        {
            this.Data.Add("ExpectedTypes", expectedTypes.ToArray());
        }

    }
}
