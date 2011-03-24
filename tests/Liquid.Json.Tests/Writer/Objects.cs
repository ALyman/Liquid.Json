using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Writer
{
    [TestClass]
    public class Objects
    {
        [TestMethod, TestCategory("Writer")]
        public void WritesEmptyObject()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteStartObject();
            writer.WriteEnd();
            Assert.AreEqual("{}", sw.ToString());
        }

        [TestMethod, TestCategory("Writer")]
        public void WritesSinglePropertyObject()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteStartObject();
            writer.WriteName("A").WriteValue(1);
            writer.WriteEnd();
            Assert.AreEqual("{\"A\": 1}", sw.ToString());
        }

        [TestMethod, TestCategory("Writer")]
        public void WritesTwoPropertyObject()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteStartObject();
            writer.WriteName("A").WriteValue(1);
            writer.WriteName("B").WriteValue(2);
            writer.WriteEnd();
            Assert.AreEqual("{\"A\": 1, \"B\": 2}", sw.ToString());
        }
    }
}