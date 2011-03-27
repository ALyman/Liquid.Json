﻿using System;
using System.Globalization;
using System.Text;

namespace Liquid.Json
{
    /// <summary>
    /// Utility methods for JSON
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// Serializes the object to a JSON-formatted value
        /// </summary>
        /// <typeparam name="T">The type of the object to be serialized</typeparam>
        /// <param name="object">The object.</param>
        /// <returns>A JSON string representing the object</returns>
        public static string ToJson<T>(this T @object)
        {
            return new JsonSerializer().Serialize(@object);
        }

        /// <summary>
        /// Escapes the string to the JSON format.
        /// </summary>
        /// <param name="str">The string to escape.</param>
        /// <returns>An escaped JSON string</returns>
        public static string EscapeString(string str)
        {
            var result = new StringBuilder(str.Length + 2);
            result.Append('"');
            foreach (char ch in str) {
                // these bounds are meant to completely skip the quotes

                switch (ch) {
                    case '"':
                        result.Append("\\\"");
                        break;
                    case '\b':
                        result.Append("\\b");
                        break;
                    case '\f':
                        result.Append("\\f");
                        break;
                    case '\n':
                        result.Append("\\n");
                        break;
                    case '\r':
                        result.Append("\\r");
                        break;
                    case '\t':
                        result.Append("\\t");
                        break;
                    default:
                        if (ch > 255) {
                            result.AppendFormat("\\u{0:x4}", (short)ch);
                        } else if (char.IsControl(ch)) {
                            result.AppendFormat("\\x{0:x2}", (byte)ch);
                        } else {
                            result.Append(ch);
                        }
                        break;
                }
            }
            result.Append('"');
            return result.ToString();
        }

        /// <summary>
        /// Unescapes the JSON-escaped string.
        /// </summary>
        /// <param name="str">The escaped string.</param>
        /// <returns>The unescaped string</returns>
        public static string UnescapeString(string str)
        {
            var result = new StringBuilder(str.Length);
            for (int i = 1; i < str.Length - 1; i++) {
                // these bounds are meant to completely skip the quotes
                switch (str[i]) {
                    case '\\':
                        switch (str[++i]) {
                            case 'b':
                                result.Append('\b');
                                break;
                            case 'f':
                                result.Append('\f');
                                break;
                            case 'n':
                                result.Append('\n');
                                break;
                            case 'r':
                                result.Append('\r');
                                break;
                            case 't':
                                result.Append('\t');
                                break;
                            case 'x':
                            case 'X':
                                var hex2 = str.Substring(i + 1, 2);
                                result.Append((char)Int32.Parse(hex2, NumberStyles.HexNumber));
                                i += 2;
                                break;
                            case 'u':
                            case 'U':
                                var hex4 = str.Substring(i + 1, 4);
                                result.Append((char)Int32.Parse(hex4, NumberStyles.HexNumber));
                                i += 4;
                                break;
                            default:
                                result.Append(str[i]);
                                break;
                        }
                        break;
                    default:
                        result.Append(str[i]);
                        break;
                }
            }
            return result.ToString();
        }
    }
}