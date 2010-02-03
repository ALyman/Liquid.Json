using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Serialization {
    [TestClass]
    public class Strings {
        public TestContext TestContext { get; set; }

        [TestMethod, TestCategory("Serialization")]
        public void Serializes_NonEscaped_String() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("\"hello, world\"", serializer.Serialize<string>("hello, world"));
        }

        [TestMethod, TestCategory("Serialization")]
        public void Escapes_Quote() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("\"this is escaped: \\\"\"", serializer.Serialize<string>("this is escaped: \""));
        }
    }
}
