using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Liquid.Json.Tests.Reader {
    [TestClass]
    public class Strings {
        public TestContext TestContext { get; set; }


        [TestMethod]
        public void ReadsNonEscapedString() {
            var sr = new StringReader("\"hello, world\"");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.String, reader.Token);
            Assert.AreEqual("\"hello, world\"", reader.Text);
            var serializer = new JsonSerializer();
        }

        [TestMethod]
        public void ReadsEscapedQuoteString() {
            var sr = new StringReader("\"this is escaped: \\\"\"");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.String, reader.Token);
            Assert.AreEqual("\"this is escaped: \\\"\"", reader.Text);
            var serializer = new JsonSerializer();
        }
    }
}
