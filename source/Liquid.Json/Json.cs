using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liquid.Json {
    public static class Json {
        public static string ToJson<T>(this T value) {
            return new JsonSerializer().Serialize(value);
        }

        public static string EscapeString(string str) {
            var result = new StringBuilder(str.Length + 2);
            result.Append('"');
            foreach (var ch in str) { // these bounds are meant to completely skip the quotes

                switch (ch) {
                    case '"': result.Append("\\\""); break;
                    case '\b': result.Append("\\\b"); break;
                    case '\f': result.Append("\\\f"); break;
                    case '\n': result.Append("\\\n"); break;
                    case '\r': result.Append("\\\r"); break;
                    case '\t': result.Append("\\\t"); break;
                    //case 'u':
                    //case 'U': throw new NotImplementedException();
                    default:
                        if (char.IsControl(ch) || ch > 255) {
                            result.AppendFormat("\\u{0:x4}", (short)ch);
                        } else {
                            result.Append(ch); 
                        }
                        break;
                }
            }
            result.Append('"');
            return result.ToString();
        }
        public static string UnescapeString(string str) {
            var result = new StringBuilder(str.Length);
            for (int i = 1; i < str.Length - 1; i++) { // these bounds are meant to completely skip the quotes
                switch (str[i]) {
                    case '\\':
                        switch (str[++i]) {
                            case 'b': result.Append('\b'); break;
                            case 'f': result.Append('\f'); break;
                            case 'n': result.Append('\n'); break;
                            case 'r': result.Append('\r'); break;
                            case 't': result.Append('\t'); break;
                            case 'u':
                            case 'U': throw new NotImplementedException();
                            default: result.Append(str[i]); break;
                        }
                        break;
                    default: result.Append(str[i]); break;
                }
            }
            return result.ToString();
        }

    }
}
