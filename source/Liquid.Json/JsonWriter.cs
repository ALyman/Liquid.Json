using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace Liquid.Json {
    public class JsonWriter {
        TextWriter writer;

        public JsonWriter(TextWriter writer) {
            this.writer = writer;
        }

        Stack<bool> state = new Stack<bool>();
        bool empty = true;
        bool itemStarted = false;
        string named = null;

        public int Depth { get { return state.Count; } }
        public bool InObject { get { return !empty && Depth > 0 && !state.Peek(); } }
        public bool InArray { get { return !empty && Depth > 0 && state.Peek(); } }

        public void WriteStartArray() {
            BeginValue();
            writer.Write("[");
            state.Push(true);
            empty = false;
            itemStarted = false;
        }
        public void WriteStartObject() {
            BeginValue();
            writer.Write("{");
            state.Push(false);
            empty = false;
            itemStarted = false;
        }
        public void WriteEnd() {
            if (InArray) {
                writer.Write(']');
            } else if (InObject) {
                writer.Write('}');
            } else throw new NotSupportedException();
            itemStarted = true;
            state.Pop();
        }

        public void WriteValue(string value) {
            BeginValue();
            writer.Write(Json.EscapeString(value));
        }

        public void WriteValue(SByte value) {
            BeginValue();
            writer.Write(value);
        }
        public void WriteValue(Int16 value) {
            BeginValue();
            writer.Write(value);
        }
        public void WriteValue(Int32 value) {
            BeginValue();
            writer.Write(value);
        }
        public void WriteValue(Int64 value) {
            BeginValue();
            writer.Write(value);
        }

        public void WriteValue(Byte value) {
            BeginValue();
            writer.Write(value);
        }
        public void WriteValue(UInt16 value) {
            BeginValue();
            writer.Write(value);
        }
        public void WriteValue(UInt32 value) {
            BeginValue();
            writer.Write(value);
        }
        public void WriteValue(UInt64 value) {
            BeginValue();
            writer.Write(value);
        }

        public void WriteValue(Single value) {
            BeginValue();
            writer.Write(value);
        }
        public void WriteValue(Double value) {
            BeginValue();
            writer.Write(value);
        }
        public void WriteValue(Decimal value) {
            BeginValue();
            writer.Write(value);
        }


        public void WriteValue(SByte value, IFormatProvider formatProvider) {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }
        public void WriteValue(Int16 value, IFormatProvider formatProvider) {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }
        public void WriteValue(Int32 value, IFormatProvider formatProvider) {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }
        public void WriteValue(Int64 value, IFormatProvider formatProvider) {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }

        public void WriteValue(Byte value, IFormatProvider formatProvider) {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }
        public void WriteValue(UInt16 value, IFormatProvider formatProvider) {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }
        public void WriteValue(UInt32 value, IFormatProvider formatProvider) {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }
        public void WriteValue(UInt64 value, IFormatProvider formatProvider) {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }

        public void WriteValue(Single value, IFormatProvider formatProvider) {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }
        public void WriteValue(Double value, IFormatProvider formatProvider) {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }
        public void WriteValue(Decimal value, IFormatProvider formatProvider) {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }

        public void WriteValue(Boolean value) {
            BeginValue();
            writer.Write(value ? "true" : "false");
        }
        public void WriteNull() {
            BeginValue();
            writer.Write("null");
        }

        private void BeginValue() {
            if (Depth == 0 && !empty) {
                throw new NotSupportedException();
            } else if (InObject) {
                if (named == null)
                    throw new Exception();
                if (itemStarted)
                    writer.Write(", ");
                writer.Write(Json.EscapeString(named));
                named = null;
                writer.Write(": ");
            } else if (InArray) {
                if (itemStarted)
                    writer.Write(", ");
            }
            empty = false;
            itemStarted = true;
        }

        public JsonWriter WriteName(string name) {
            if (!InObject)
                throw new NotSupportedException();
            this.named = name;
            return this;
        }

        [Obsolete("Do not use this!", false)]
        public void WriteLiteralValue(string str) {
            BeginValue();
            writer.Write(str);
        }
    }
}
