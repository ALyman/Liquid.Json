using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liquid.Json.TypeSerializers {
    class JsonStringSerializer : IJsonTypeSerializer<string> {
        public void Serialize(string value, JsonSerializationContext context) {
            context.Write('"');
            foreach (char ch in value) {
                switch (ch) {
                    case '"': context.Write("\\\""); break;
                    default: context.Write(ch); break;
                }
            }
            context.Write('"');
        }


        public string Deserialize(JsonDeserializationContext context) {
            var str = context.Reader.ReadNextAs(JsonTokenType.String);
            return Unescape(str);
        }

        public static string Unescape(string str) {
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
