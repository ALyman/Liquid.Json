using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liquid.Json {
    /// <summary>
    /// A reader class for JSON-formatted data
    /// </summary>
    public class JsonReader {
        TextReader reader;
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonReader"/> class.
        /// </summary>
        /// <param name="reader">The reader that contains the data.</param>
        public JsonReader(TextReader reader) {
            this.reader = reader;
        }

        /// <summary>
        /// Gets or sets the token type.
        /// </summary>
        /// <value>The token type.</value>
        public JsonTokenType Token { get; private set; }
        /// <summary>
        /// Gets the text for the current token.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get { return buffer.ToString(); } }

        StringBuilder buffer = new StringBuilder();

        bool undone = false;

        /// <summary>
        /// Reads the next token.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if there was a token to be read; otherwise, <c>false</c>.
        /// </returns>
        public bool ReadNext() {
            if (undone) {
                undone = false;
                return true;
            }
            char ch;
            do {
                ch = (char)Peek();
            } while (char.IsWhiteSpace(ch) && Read() >= '\0');
            buffer.Length = 0;
            switch (ch) {
                case '\0': return false;
                case '{': Read(); Token = JsonTokenType.ObjectStart; return true;
                case '}': Read(); Token = JsonTokenType.ObjectEnd; return true;
                case '[': Read(); Token = JsonTokenType.ArrayStart; return true;
                case ']': Read(); Token = JsonTokenType.ArrayEnd; return true;
                case ',': Read(); Token = JsonTokenType.Comma; return true;
                case ':': Read(); Token = JsonTokenType.Colon; return true;
                case '"': ReadString(); return true;
                default:
                    if (ch == '-' || char.IsNumber(ch)) {
                        ReadNumber();
                        return true;
                    } else if (char.IsLetter(ch)) {
                        ReadIdentifier();
                        return true;
                    }
                    throw new NotImplementedException();
            }
        }
        /// <summary>
        /// Reads the next token, and matches it to a set of expected token types.
        /// </summary>
        /// <param name="expectedTypes">The expected types.</param>
        /// <returns>The text of the token</returns>
        public string ReadNextAs(params JsonTokenType[] expectedTypes) {
            if (!ReadNext())
                throw new JsonDeserializationException();
            if (Array.IndexOf(expectedTypes, Token) == -1)
                throw new JsonDeserializationException();
            return Text;
        }

        /// <summary>
        /// Undoes the last read.
        /// </summary>
        public void UndoRead() {
            if (undone)
                throw new NotSupportedException();
            undone = true;
        }

        private void ReadIdentifier() {
            var ch = Peek();
            while (char.IsLetter(ch)) { Read(); ch = Peek(); }
            switch (Text) {
                case "new": Token = JsonTokenType.New; break;
                case "true": Token = JsonTokenType.True; break;
                case "false": Token = JsonTokenType.False; break;
                default: Token = JsonTokenType.Identifier; break;
            }
        }
        private void ReadString() {
            Token = JsonTokenType.String;
            var ch = Peek();
            if (ch != '"') throw new Exception();
            Read(); ch = Peek();
            while (ch != '"') {
                if (ch == '\\') { Read(); ch = Peek(); }
                Read(); ch = Peek();
            }
            Read();
        }
        private void ReadNumber() {
            Token = JsonTokenType.Integer;
            var ch = Peek();
            if (ch == '-') { Read(); ch = Peek(); }
            while (char.IsNumber(ch)) {
                Read(); ch = Peek();
            }
            if (ch == '.') {
                Token = JsonTokenType.Real;
                Read(); ch = Peek();
                while (char.IsNumber(ch)) {
                    Read(); ch = Peek();
                }
            }
            if (ch == 'e' || ch == 'E') {
                Token = JsonTokenType.Real;
                Read(); ch = Peek();
                if (ch == '-' || ch == '+') { Read(); ch = Peek(); }
                while (char.IsNumber(ch)) {
                    Read(); ch = Peek();
                }
            }
        }

        private char Read() {
            var ch = (char)Peek();
            reader.Read();
            buffer.Append(ch);
            return ch;
        }
        private char Peek() {
            var ch = reader.Peek();
            if (ch == -1)
                return '\0';
            else
                return (char)ch;
        }
    }
}
