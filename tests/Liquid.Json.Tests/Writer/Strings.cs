﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Liquid.Json.Tests.Writer {
    [TestClass]
    public class Strings {
        [TestMethod, TestCategory("Writer")]
        public void WritesNonEscapedString() {
            var sw = new StringWriter();
            var writer= new JsonWriter(sw);
            writer.WriteValue("hello, world");
            Assert.AreEqual("\"hello, world\"", sw.ToString());
            var serializer = new JsonSerializer();
        }

        [TestMethod, TestCategory("Writer")]
        public void WritesEscapedQuoteString() {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue("this is escaped: \"");
            Assert.AreEqual("\"this is escaped: \\\"\"", sw.ToString());
        }
    }
}