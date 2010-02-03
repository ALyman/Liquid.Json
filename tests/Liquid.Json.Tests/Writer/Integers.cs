using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Liquid.Json.Tests.Writer {
    [TestClass]
    public class Integers {
        [TestMethod, TestCategory("Writer")]
        public void WritesInt32() {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue((Int32)123);
            Assert.AreEqual("123", sw.ToString());
        }
        [TestMethod, TestCategory("Writer")]
        public void WritesInt16() {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue((Int16)123);
            Assert.AreEqual("123", sw.ToString());
        }
    }
}
