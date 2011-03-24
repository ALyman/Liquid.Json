﻿using System;
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
    }
}