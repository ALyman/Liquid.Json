using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Writer
{
    [TestClass]
    public class Constructors
    {
        [TestMethod, TestCategory("Writer")]
        public void WritesEmptyConstructor()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteStartConstructor("XXX");
            writer.WriteEnd();
            Assert.AreEqual("new XXX()", sw.ToString());
        }

        [TestMethod, TestCategory("Writer")]
        public void WritesSinglePropertyConstructor()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteStartConstructor("YYY");
            writer.WriteValue(1);
            writer.WriteEnd();
            Assert.AreEqual("new YYY(1)", sw.ToString());
        }

        [TestMethod, TestCategory("Writer")]
        public void WritesTwoPropertyConstructor()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteStartConstructor("ZZZ");
            writer.WriteValue(1);
            writer.WriteValue(2);
            writer.WriteEnd();
            Assert.AreEqual("new ZZZ(1, 2)", sw.ToString());
        }
    }
}