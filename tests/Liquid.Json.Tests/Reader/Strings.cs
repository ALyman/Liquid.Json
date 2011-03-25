using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Reader
{
    [TestClass]
    public class Strings
    {
        public TestContext TestContext { get; set; }

        [TestMethod, TestCategory("Reader")]
        public void ReadsNonEscapedString()
        {
            var sr = new StringReader("\"hello, world\"");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.String, reader.Token);
            Assert.AreEqual("\"hello, world\"", reader.Text);
        }

        [TestMethod, TestCategory("Reader")]
        public void ReadsEscapedQuoteString()
        {
            var sr = new StringReader("\"this is escaped: \\\"\"");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.String, reader.Token);
            Assert.AreEqual("\"this is escaped: \\\"\"", reader.Text);
        }

        [TestMethod, TestCategory("Writer")]
        public void ReadsEscapedControlCharacters()
        {
            var sr = new StringReader("\"this is escaped: \\b\\f\\n\\r\\t\\x17\"");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.String, reader.Token);
            Assert.AreEqual("\"this is escaped: \\b\\f\\n\\r\\t\\x17\"", reader.Text);
        }

        [TestMethod, TestCategory("Writer")]
        public void ReadsEscapedUnicodeCharacters()
        {
            var sr = new StringReader("\"this is escaped: \\u3034\"");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.String, reader.Token);
            Assert.AreEqual("\"this is escaped: \\u3034\"", reader.Text);
        }

        [TestMethod]
        public void UnescapesControlCharacters()
        {
            Assert.AreEqual("this is escaped: $\b\f\n\r\t\x17", Json.UnescapeString("\"this is escaped: \\$\\b\\f\\n\\r\\t\\x17\""));
        }

        [TestMethod]
        public void UnescapesUnicodeCharacters()
        {
            Assert.AreEqual("this is escaped: \u3034", Json.UnescapeString("\"this is escaped: \\u3034\""));
        }
    }
}