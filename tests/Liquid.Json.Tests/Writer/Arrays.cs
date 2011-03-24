using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Writer
{
    [TestClass]
    public class Arrays
    {
        [TestMethod, TestCategory("Writer")]
        public void WritesEmptyArray()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteStartArray();
            writer.WriteEnd();
            Assert.AreEqual("[]", sw.ToString());
        }

        [TestMethod, TestCategory("Writer")]
        public void WritesSinglePropertyArray()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteStartArray();
            writer.WriteValue(1);
            writer.WriteEnd();
            Assert.AreEqual("[1]", sw.ToString());
        }

        [TestMethod, TestCategory("Writer")]
        public void WritesTwoPropertyArray()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteStartArray();
            writer.WriteValue(1);
            writer.WriteValue(2);
            writer.WriteEnd();
            Assert.AreEqual("[1, 2]", sw.ToString());
        }
    }
}