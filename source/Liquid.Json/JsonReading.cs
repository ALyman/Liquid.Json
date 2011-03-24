using System;

namespace Liquid.Json
{
    internal static class JsonReading
    {
        /// <summary>
        /// Reads the next token, and matches it to a set of expected token types.
        /// </summary>
        /// <param name="reader">The JsonReader to read a token from.</param>
        /// <param name="expectedTypes">The expected types.</param>
        /// <returns>The text of the token</returns>
        public static string ReadNextAs(this JsonReader reader, params JsonTokenType[] expectedTypes)
        {
            if (!reader.ReadNext())
                throw new JsonDeserializationException();
            if (Array.IndexOf(expectedTypes, reader.Token) ==
                -1)
                throw new JsonDeserializationException();
            return reader.Text;
        }
    }
}