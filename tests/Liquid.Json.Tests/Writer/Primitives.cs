using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Writer
{
    [TestClass]
    public class Primitives
    {
        [TestMethod]
        public void WritesBooleanTrue()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue(true);
            Assert.AreEqual("true", sw.ToString());
        }

        [TestMethod]
        public void WritesBooleanFalse()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue(false);
            Assert.AreEqual("false", sw.ToString());
        }

        [TestMethod]
        public void WritesNull()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteNull();
            Assert.AreEqual("null", sw.ToString());
        }
    }
}