using System;
using System.Globalization;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Writer
{
    [TestClass]
    public class Reals
    {
        [TestMethod]
        public void WritesSingle()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue(123.45f);
            Assert.AreEqual("123.45", sw.ToString());
        }

        [TestMethod]
        public void WritesDouble()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue(123.45);
            Assert.AreEqual("123.45", sw.ToString());
        }

        [TestMethod]
        public void WritesDecimal()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue(123.45m);
            Assert.AreEqual("123.45", sw.ToString());
        }

        [TestMethod]
        public void WritesSingleWithFormat()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue(123.45f, NumberFormatInfo.GetInstance(null));
            Assert.AreEqual("123.45", sw.ToString());
        }

        [TestMethod]
        public void WritesDoubleWithFormat()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue(123.45, NumberFormatInfo.GetInstance(null));
            Assert.AreEqual("123.45", sw.ToString());
        }

        [TestMethod]
        public void WritesDecimalWithFormat()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue(123.45m, NumberFormatInfo.GetInstance(null));
            Assert.AreEqual("123.45", sw.ToString());
        }
    }
}