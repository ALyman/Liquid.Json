using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Reader
{
    [TestClass]
    public class Keywords
    {
        [TestMethod, TestCategory("Reader")]
        public void ReadsNew()
        {
            var sr = new StringReader("new Date()");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.New, reader.Token);
            Assert.AreEqual("new", reader.Text);
        }

        [TestMethod, TestCategory("Reader")]
        public void ReadsTrue()
        {
            var sr = new StringReader("true");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.True, reader.Token);
            Assert.AreEqual("true", reader.Text);
        }

        [TestMethod, TestCategory("Reader")]
        public void ReadsFalse()
        {
            var sr = new StringReader("false");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.False, reader.Token);
            Assert.AreEqual("false", reader.Text);
        }

        [TestMethod, TestCategory("Reader")]
        public void ReadsIdentifier()
        {
            var sr = new StringReader("Date");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Identifier, reader.Token);
            Assert.AreEqual("Date", reader.Text);
        }
    }
}