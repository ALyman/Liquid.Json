using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Writer
{
    [TestClass]
    public class Integers
    {
        [TestMethod, TestCategory("Writer")]
        public void WritesInt32()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue(123);
            Assert.AreEqual("123", sw.ToString());
        }

        [TestMethod, TestCategory("Writer")]
        public void WritesInt16()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue((Int16)123);
            Assert.AreEqual("123", sw.ToString());
        }

        [TestMethod, TestCategory("Writer")]
        public void WritesInt64()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue((Int64)123);
            Assert.AreEqual("123", sw.ToString());
        }

        [TestMethod, TestCategory("Writer")]
        public void WritesSByte()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue((SByte)123);
            Assert.AreEqual("123", sw.ToString());
        }

        [TestMethod, TestCategory("Writer")]
        public void WritesUInt32()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue((UInt32)123);
            Assert.AreEqual("123", sw.ToString());
        }

        [TestMethod, TestCategory("Writer")]
        public void WritesUInt16()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue((UInt16)123);
            Assert.AreEqual("123", sw.ToString());
        }

        [TestMethod, TestCategory("Writer")]
        public void WritesUInt64()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue((UInt64)123);
            Assert.AreEqual("123", sw.ToString());
        }

        [TestMethod, TestCategory("Writer")]
        public void WritesByte()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue((Byte)123);
            Assert.AreEqual("123", sw.ToString());
        }
    }
}