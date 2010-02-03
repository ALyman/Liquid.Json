﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Liquid.Json.Tests.Reader {
    [TestClass]
    public class Keywords {
        [TestMethod]
        public void ReadsNew() {
            var sr = new StringReader("new Date()");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.New, reader.Token);
            Assert.AreEqual("new", reader.Text);
        }

        [TestMethod]
        public void ReadsTrue() {
            var sr = new StringReader("true");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.True, reader.Token);
            Assert.AreEqual("true", reader.Text);
        }

        [TestMethod]
        public void ReadsFalse() {
            var sr = new StringReader("false");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.False, reader.Token);
            Assert.AreEqual("false", reader.Text);
        }

        [TestMethod]
        public void ReadsIdentifier() {
            var sr = new StringReader("Date");
            var reader = new JsonReader(sr);
            Assert.IsTrue(reader.ReadNext(), "Failed read");
            Assert.AreEqual(JsonTokenType.Identifier, reader.Token);
            Assert.AreEqual("Date", reader.Text);
        }
    }
}
