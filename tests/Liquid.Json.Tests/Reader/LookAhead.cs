using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Reader
{
    [TestClass]
    public class LookAhead
    {
        [TestMethod, TestCategory("Reader")]
        public void LookingAhead()
        {
            var sr = new StringReader("Date");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Identifier, reader.Token);
            Assert.AreEqual("Date", reader.Text);

            reader.UndoRead();

            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Identifier, reader.Token);
            Assert.AreEqual("Date", reader.Text);
        }

        [TestMethod, TestCategory("Reader")]
        public void CanLookAheadAtMostOnce()
        {
            var sr = new StringReader("Date");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Identifier, reader.Token);
            Assert.AreEqual("Date", reader.Text);

            reader.UndoRead();

            try {
                reader.UndoRead();
                Assert.Fail("Did not throw the expected exception");
            } catch (NotSupportedException ex) {
                Assert.AreEqual("Can only UndoRead one token at a time", ex.Message);
            }
        }

        [TestMethod, TestCategory("Reader")]
        public void ReadNextAsFailsAtEndOfStream()
        {
            var sr = new StringReader("Date");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");

            try {
                var text = reader.ReadNextAs(JsonTokenType.True);
                Assert.Fail("ReadNextAs found text: {0} past the end of the token stream", text);
            } catch (JsonDeserializationException ex) {
                Assert.AreEqual("Unexpected end of file, expected: 'true'", ex.Message);
            }
        }

        [TestMethod, TestCategory("Reader")]
        public void ReadNextAsFailsOnUnexpectedInput()
        {
            var sr = new StringReader("Date");
            var reader = new JsonReader(sr);
            try {
                var text = reader.ReadNextAs(JsonTokenType.Integer, JsonTokenType.ObjectStart, JsonTokenType.Real);
                Assert.Fail("ReadNextAs found an unexpected match: {0}", text);
            } catch (JsonUnexpectedTokenException ex) {
                Assert.AreEqual("Unexpected token 'Date', expected: '{', integer, real", ex.Message);
            }
        }
    }
}