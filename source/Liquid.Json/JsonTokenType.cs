using System;

namespace Liquid.Json
{
    /// <summary>
    /// A list of JSON token types
    /// </summary>
    public enum JsonTokenType
    {
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
        Colon
    }
}