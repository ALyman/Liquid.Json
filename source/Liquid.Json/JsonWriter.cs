using System;
using System.Collections.Generic;
using System.IO;

namespace Liquid.Json
{
    /// <summary>
    /// Writes data in the JSON format
    /// </summary>
    public class JsonWriter
    {
        private readonly Stack<NodeType> state = new Stack<NodeType>();
        private readonly TextWriter writer;
        private bool empty = true;
        private bool itemStarted;
        private string named;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonWriter"/> class.
        /// </summary>
        /// <param name="writer">The writer in which data is written.</param>
        public JsonWriter(TextWriter writer)
        {
            this.writer = writer;
        }

        /// <summary>
        /// Gets the current depth of the writer.
        /// </summary>
        /// <value>The depth.</value>
        public int Depth { get { return state.Count; } }

        /// <summary>
        /// Gets a value indicating whether the writer is in an object.
        /// </summary>
        /// <value><c>true</c> if in an object; otherwise, <c>false</c>.</value>
        public bool InObject { get { return !empty && Depth > 0 && state.Peek() == NodeType.Object; } }

        /// <summary>
        /// Gets a value indicating whether in an array.
        /// </summary>
        /// <value><c>true</c> if in an array; otherwise, <c>false</c>.</value>
        public bool InArray { get { return !empty && Depth > 0 && state.Peek() == NodeType.Array; } }

        /// <summary>
        /// Gets a value indicating whether in a constructor.
        /// </summary>
        /// <value><c>true</c> if in a constructor; otherwise, <c>false</c>.</value>
        public bool InConstructor { get { return !empty && Depth > 0 && state.Peek() == NodeType.Constructor; } }

        /// <summary>
        /// Starts an array.
        /// </summary>
        public void WriteStartArray()
        {
            BeginValue();
            writer.Write("[");
            state.Push(NodeType.Array);
            empty = false;
            itemStarted = false;
        }

        /// <summary>
        /// Starts an object.
        /// </summary>
        public void WriteStartObject()
        {
            BeginValue();
            writer.Write("{");
            state.Push(NodeType.Object);
            empty = false;
            itemStarted = false;
        }

        /// <summary>
        /// Starts a constructor
        /// </summary>
        /// <param name="typeName">Name of the type being constructed.</param>
        public void WriteStartConstructor(string typeName)
        {
            BeginValue();
            writer.Write("new ");
            writer.Write(typeName);
            writer.Write('(');
            state.Push(NodeType.Constructor);
            empty = false;
            itemStarted = false;
        }

        /// <summary>
        /// Writes the end of the current object, array, or constructor.
        /// </summary>
        public void WriteEnd()
        {
            if (InArray) {
                writer.Write(']');
            } else if (InObject) {
                writer.Write('}');
            } else if (InConstructor) {
                writer.Write(')');
            } else throw new NotSupportedException("Need to be inside an Array, Object or Constructor to End");
            itemStarted = true;
            state.Pop();
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteValue(string value)
        {
            BeginValue();
            writer.Write(Json.EscapeString(value));
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteValue(SByte value)
        {
            BeginValue();
            writer.Write(value);
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteValue(Int16 value)
        {
            BeginValue();
            writer.Write(value);
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteValue(Int32 value)
        {
            BeginValue();
            writer.Write(value);
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteValue(Int64 value)
        {
            BeginValue();
            writer.Write(value);
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteValue(Byte value)
        {
            BeginValue();
            writer.Write(value);
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteValue(UInt16 value)
        {
            BeginValue();
            writer.Write(value);
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteValue(UInt32 value)
        {
            BeginValue();
            writer.Write(value);
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteValue(UInt64 value)
        {
            BeginValue();
            writer.Write(value);
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteValue(Single value)
        {
            BeginValue();
            writer.Write(value);
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteValue(Double value)
        {
            BeginValue();
            writer.Write(value);
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteValue(Decimal value)
        {
            BeginValue();
            writer.Write(value);
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="formatProvider">The format provider.</param>
        public void WriteValue(SByte value, IFormatProvider formatProvider)
        {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="formatProvider">The format provider.</param>
        public void WriteValue(Int16 value, IFormatProvider formatProvider)
        {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="formatProvider">The format provider.</param>
        public void WriteValue(Int32 value, IFormatProvider formatProvider)
        {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="formatProvider">The format provider.</param>
        public void WriteValue(Int64 value, IFormatProvider formatProvider)
        {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="formatProvider">The format provider.</param>
        public void WriteValue(Byte value, IFormatProvider formatProvider)
        {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="formatProvider">The format provider.</param>
        public void WriteValue(UInt16 value, IFormatProvider formatProvider)
        {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="formatProvider">The format provider.</param>
        public void WriteValue(UInt32 value, IFormatProvider formatProvider)
        {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="formatProvider">The format provider.</param>
        public void WriteValue(UInt64 value, IFormatProvider formatProvider)
        {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="formatProvider">The format provider.</param>
        public void WriteValue(Single value, IFormatProvider formatProvider)
        {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="formatProvider">The format provider.</param>
        public void WriteValue(Double value, IFormatProvider formatProvider)
        {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="formatProvider">The format provider.</param>
        public void WriteValue(Decimal value, IFormatProvider formatProvider)
        {
            BeginValue();
            writer.Write(value.ToString(formatProvider));
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public void WriteValue(Boolean value)
        {
            BeginValue();
            writer.Write(value ? "true" : "false");
        }

        /// <summary>
        /// Writes the null.
        /// </summary>
        public void WriteNull()
        {
            BeginValue();
            writer.Write("null");
        }

        private void BeginValue()
        {
            if (Depth == 0 &&
                !empty) {
                throw new NotSupportedException("Can not have two values at the root");
            } else if (InObject) {
                if (named == null)
                    throw new NotSupportedException("Object properties must have a name written first");
                if (itemStarted)
                    writer.Write(", ");
                writer.Write(Json.EscapeString(named));
                named = null;
                writer.Write(": ");
            } else if (InArray || InConstructor) {
                if (itemStarted)
                    writer.Write(", ");
            }
            empty = false;
            itemStarted = true;
        }

        /// <summary>
        /// Writes the name of an object property.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>This JsonWriter</returns>
        public JsonWriter WriteName(string name)
        {
            if (!InObject)
                throw new NotSupportedException("Must be inside an object to write a name");
            named = name;
            return this;
        }

        #region Nested type: NodeType

        private enum NodeType
        {
            Array,
            Object,
            Constructor
        }

        #endregion
    }
}