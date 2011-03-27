using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Linq;

namespace Liquid.Json
{
    /// <summary>
    /// A reader class for JSON-formatted data
    /// </summary>
    public class JsonReader
    {
        private readonly StringBuilder buffer = new StringBuilder();
        private readonly TextReader reader;
        private bool undone;
        private bool readEOF;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonReader"/> class.
        /// </summary>
        /// <param name="reader">The reader that contains the data.</param>
        public JsonReader(TextReader reader)
        {
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

        /// <summary>
        /// Reads the next token.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if there was a token to be read; otherwise, <c>false</c>.
        /// </returns>
        public bool ReadNext()
        {
            if (undone) {
                undone = false;
                return true;
            }
            char ch;
            do {
                ch = Peek();
            } while (char.IsWhiteSpace(ch) &&
                     Read() >= '\0');
            buffer.Length = 0;
            switch (ch) {
                case '\0':
                    readEOF = true;
                    return false;
                case '(':
                    Read();
                    Token = JsonTokenType.ConstructorStart;
                    return true;
                case ')':
                    Read();
                    Token = JsonTokenType.ConstructorEnd;
                    return true;
                case '{':
                    Read();
                    Token = JsonTokenType.ObjectStart;
                    return true;
                case '}':
                    Read();
                    Token = JsonTokenType.ObjectEnd;
                    return true;
                case '[':
                    Read();
                    Token = JsonTokenType.ArrayStart;
                    return true;
                case ']':
                    Read();
                    Token = JsonTokenType.ArrayEnd;
                    return true;
                case ',':
                    Read();
                    Token = JsonTokenType.Comma;
                    return true;
                case ':':
                    Read();
                    Token = JsonTokenType.Colon;
                    return true;
                case '"':
                    ReadString();
                    return true;
                default:
                    if (ch == '-' ||
                        char.IsNumber(ch)) {
                        ReadNumber();
                        return true;
                    } else if (char.IsLetter(ch)) {
                        ReadIdentifier();
                        return true;
                    }
                    throw new FormatException(string.Format("Invalid character: {0}", ch));
            }
        }

        /// <summary>
        /// Undoes the last read.
        /// </summary>
        public void UndoRead()
        {
            if (undone)
                throw new NotSupportedException("Can only UndoRead one token at a time");
            undone = true;
        }

        private void ReadIdentifier()
        {
            char ch = Peek();
            while (char.IsLetter(ch)) {
                Read();
                ch = Peek();
            }
            switch (Text) {
                case "new":
                    Token = JsonTokenType.New;
                    break;
                case "true":
                    Token = JsonTokenType.True;
                    break;
                case "false":
                    Token = JsonTokenType.False;
                    break;
                default:
                    Token = JsonTokenType.Identifier;
                    break;
            }
        }

        private void ReadString()
        {
            Token = JsonTokenType.String;
            char ch = Peek();
            Debug.Assert(ch == '"');
            Read();
            ch = Peek();
            while (ch != '"') {
                if (ch == '\\') {
                    Read();
                    ch = Peek();
                }
                Read();
                ch = Peek();
            }
            Read();
        }

        private void ReadNumber()
        {
            Token = JsonTokenType.Integer;
            char ch = Peek();
            if (ch == '-') {
                Read();
                ch = Peek();
            }
            while (char.IsNumber(ch)) {
                Read();
                ch = Peek();
            }
            if (ch == '.') {
                Token = JsonTokenType.Real;
                Read();
                ch = Peek();
                while (char.IsNumber(ch)) {
                    Read();
                    ch = Peek();
                }
            }
            if (ch == 'e' ||
                ch == 'E') {
                Token = JsonTokenType.Real;
                Read();
                ch = Peek();
                if (ch == '-' ||
                    ch == '+') {
                    Read();
                    ch = Peek();
                }
                while (char.IsNumber(ch)) {
                    Read();
                    ch = Peek();
                }
            }
        }

        private char Read()
        {
            char ch = Peek();
            reader.Read();
            buffer.Append(ch);
            return ch;
        }

        private char Peek()
        {
            int ch = reader.Peek();
            if (ch == -1)
                return '\0';
            else
                return (char)ch;
        }

        /// <summary>
        /// Gets a value indicating whether the reader is at the end of the stream.
        /// </summary>
        /// <value><c>true</c> if at the end of the stream; otherwise, <c>false</c>.</value>
        public bool AtEndOfStream { get { return readEOF & !undone; } }
    }
}