using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Reader
{
    [TestClass]
    public class Reals
    {
        [TestMethod, TestCategory("Reader")]
        public void ReadsSimpleReal()
        {
            var sr = new StringReader("123.0");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Real, reader.Token);
            Assert.AreEqual("123.0", reader.Text);
        }

        [TestMethod, TestCategory("Reader")]
        public void ReadsSimpleNegativeReal()
        {
            var sr = new StringReader("-123.0");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Real, reader.Token);
            Assert.AreEqual("-123.0", reader.Text);
        }

        [TestMethod, TestCategory("Reader")]
        public void ReadsExponentialReal()
        {
            var sr = new StringReader("123.0e12");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Real, reader.Token);
            Assert.AreEqual("123.0e12", reader.Text);
        }

        [TestMethod, TestCategory("Reader")]
        public void ReadsExponentialReal2()
        {
            var sr = new StringReader("123.0e-12");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Real, reader.Token);
            Assert.AreEqual("123.0e-12", reader.Text);
        }

        [TestMethod, TestCategory("Reader")]
        public void ReadsExponentialReal3()
        {
            var sr = new StringReader("123.0e+12");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Real, reader.Token);
            Assert.AreEqual("123.0e+12", reader.Text);
        }

        [TestMethod, TestCategory("Reader")]
        public void ReadsExponentialNegativeReal()
        {
            var sr = new StringReader("-123.0e1");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Real, reader.Token);
            Assert.AreEqual("-123.0e1", reader.Text);
        }
    }
}