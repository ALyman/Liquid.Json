using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Serialization {
    [TestClass]
    public class Arrays {
        public TestContext TestContext { get; set; }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesArray() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("[0, 1, 2]", serializer.Serialize(new Int32[] { 0, 1, 2 }));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesGenericList() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("[0, 1, 2]", serializer.Serialize(new List<Int32> { 0, 1, 2 }));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesGenericEnumerable() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("[0, 1, 2]", serializer.Serialize(new Int32[] { 0, 1, 2 }.Select(i => i)));
        }

        [TestMethod, TestCategory("Serialization")]
        public void NullArray() {
            var serializer = new JsonSerializer();
            var result = serializer.Serialize<int[]>(null);
            Assert.AreEqual("null", result);
        }
    }
}
