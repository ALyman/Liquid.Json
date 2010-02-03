﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Liquid.Json;

namespace Liquid.Json.Tests.Reader {
    [TestClass]
    public class Integers {
        [TestMethod, TestCategory("Reader")]
        public void ReadsInteger() {
            var sr = new StringReader("123");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Integer, reader.Token);
            Assert.AreEqual("123", reader.Text);
        }
        [TestMethod, TestCategory("Reader")]
        public void ReadsNegativeInteger() {
            var sr = new StringReader("-123");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Integer, reader.Token);
            Assert.AreEqual("-123", reader.Text);
        }
    }
}
