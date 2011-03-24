using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liquid.Json
{
    /// <summary>
    /// An exception thrown when we received a token we did not expect
    /// </summary>
    public class JsonUnexpectedTokenException : JsonDeserializationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonUnexpectedTokenException"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="text">The text.</param>
        /// <param name="expectedTypes">The expected types.</param>
        public JsonUnexpectedTokenException(JsonTokenType token, string text, params JsonTokenType[] expectedTypes)
            : this(token, text, expectedTypes.AsEnumerable())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonUnexpectedTokenException"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="text">The text.</param>
        /// <param name="expectedTypes">The expected types.</param>
        public JsonUnexpectedTokenException(JsonTokenType token, string text, IEnumerable<JsonTokenType> expectedTypes)
            : base(string.Format("Unexpected token '{0}', expected: {1}", text, expectedTypes.ToErrorString()))
        {
            this.Data.Add("Token", token);
            this.Data.Add("Text", text);
            this.Data.Add("ExpectedTypes", expectedTypes.ToArray());
        }
    }
}
