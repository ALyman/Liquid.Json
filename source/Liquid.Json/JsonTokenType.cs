using System;
using System.Collections.Generic;
using System.Linq;

namespace Liquid.Json
{
    /// <summary>
    /// A list of JSON token types
    /// </summary>
    public enum JsonTokenType
    {
        /// <summary>
        /// Any token type
        /// </summary>
        Any = -1,

        /// <summary>
        /// '{'
        /// </summary>
        ObjectStart,
        /// <summary>
        /// '}'
        /// </summary>
        ObjectEnd,
        /// <summary>
        /// '['
        /// </summary>
        ArrayStart,
        /// <summary>
        /// ']'
        /// </summary>
        ArrayEnd,
        /// <summary>
        /// Integer
        /// </summary>
        Integer,
        /// <summary>
        /// Real
        /// </summary>
        Real,
        /// <summary>
        /// String
        /// </summary>
        String,
        /// <summary>
        /// Identifier
        /// </summary>
        Identifier,
        /// <summary>
        /// 'true'
        /// </summary>
        True,
        /// <summary>
        /// 'false'
        /// </summary>
        False,
        /// <summary>
        /// 'new'
        /// </summary>
        New,
        /// <summary>
        /// ','
        /// </summary>
        Comma,
        /// <summary>
        /// ':'
        /// </summary>
        Colon,
        ConstructorStart,
        ConstructorEnd,
    }

    internal static class JsonTokenTypes
    {
        public static string ToErrorString(this IEnumerable<JsonTokenType> types, string expectedText = null)
        {
            if (expectedText != null)
                return string.Format(", expected: '{0}'", expectedText);

            if (types.Contains(JsonTokenType.Any))
                return string.Empty;

            return ", expected: " + string.Join(
                ", ",
                types
                    .OrderBy(e => e)
                    .Distinct()
                    .Select(e => e.ToErrorString())
                    .ToArray()
                );
        }
        public static string ToErrorString(this JsonTokenType type)
        {
            switch (type) {
                case JsonTokenType.ObjectStart:
                    return "'{'";
                case JsonTokenType.ObjectEnd:
                    return "'}'";
                case JsonTokenType.ArrayStart:
                    return "'['";
                case JsonTokenType.ArrayEnd:
                    return "']'";
                case JsonTokenType.Integer:
                    return "integer";
                case JsonTokenType.Real:
                    return "real";
                case JsonTokenType.String:
                    return "string";
                case JsonTokenType.Identifier:
                    return "identifier";
                case JsonTokenType.True:
                    return "'true'";
                case JsonTokenType.False:
                    return "'false'";
                case JsonTokenType.New:
                    return "'new'";
                case JsonTokenType.Comma:
                    return "','";
                case JsonTokenType.Colon:
                    return "':'";
                default:
                    return type.ToString();
            }
        }
    }
}