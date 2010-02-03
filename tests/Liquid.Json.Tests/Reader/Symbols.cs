using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Liquid.Json.Tests.Reader {
    [TestClass]
    public class Symbols {
        [TestMethod]
        public void ReadsObjectStart() {
            var sr = new StringReader("{");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.ObjectStart, reader.Token);
            Assert.AreEqual("{", reader.Text);
        }

        [TestMethod]
        public void ReadsObjectEnd() {
            var sr = new StringReader("}");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.ObjectEnd, reader.Token);
            Assert.AreEqual("}", reader.Text);
        }

        [TestMethod]
        public void ReadsArrayStart() {
            var sr = new StringReader("[");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.ArrayStart, reader.Token);
            Assert.AreEqual("[", reader.Text);
        }

        [TestMethod]
        public void ReadsArrayEnd() {
            var sr = new StringReader("]");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.ArrayEnd, reader.Token);
            Assert.AreEqual("]", reader.Text);
        }

        [TestMethod]
        public void ReadsComma() {
            var sr = new StringReader(",");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Comma, reader.Token);
            Assert.AreEqual(",", reader.Text);
        }

        [TestMethod]
        public void ReadsColon() {
            var sr = new StringReader(":");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Colon, reader.Token);
            Assert.AreEqual(":", reader.Text);
        }
    }
}
