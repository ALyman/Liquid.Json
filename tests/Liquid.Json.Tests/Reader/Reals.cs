using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Liquid.Json.Tests.Reader {
    [TestClass]
    public class Reals {
        [TestMethod]
        public void ReadsSimpleReal() {
            var sr = new StringReader("123.0");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Real, reader.Token);
            Assert.AreEqual("123.0", reader.Text);
        }
        [TestMethod]
        public void ReadsSimpleNegativeReal() {
            var sr = new StringReader("-123.0");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Real, reader.Token);
            Assert.AreEqual("-123.0", reader.Text);
        }
        [TestMethod]
        public void ReadsExponentialReal() {
            var sr = new StringReader("123.0e12");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Real, reader.Token);
            Assert.AreEqual("123.0e12", reader.Text);
        }
        [TestMethod]
        public void ReadsExponentialReal2() {
            var sr = new StringReader("123.0e-12");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Real, reader.Token);
            Assert.AreEqual("123.0e-12", reader.Text);
        }
        [TestMethod]
        public void ReadsExponentialReal3() {
            var sr = new StringReader("123.0e+12");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Real, reader.Token);
            Assert.AreEqual("123.0e+12", reader.Text);
        }
        [TestMethod]
        public void ReadsExponentialNegativeReal() {
            var sr = new StringReader("-123.0e1");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Real, reader.Token);
            Assert.AreEqual("-123.0e1", reader.Text);
        }
    }
}
