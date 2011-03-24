using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Reader
{
    [TestClass]
    public class Malformed
    {
        [TestMethod, TestCategory("Reader")]
        public void NULTerminates()
        {
            var sr = new StringReader("\0");
            var reader = new JsonReader(sr);
            Assert.IsFalse(reader.ReadNext());
        }

        [TestMethod, TestCategory("Reader")]
        public void BadCharactersThrow()
        {
            var badChars = new[] { '|', '#', '@', '&' };
            foreach (char ch in badChars) {
                var sr = new StringReader(new string(ch, 1));
                var reader = new JsonReader(sr);
                try {
                    reader.ReadNext();
                    Assert.Fail("Did not throw the expected FormatException");
                } catch (FormatException ex) {
                    Assert.AreEqual("Invalid character: " + ch, ex.Message);
                }
            }
        }
    }
}