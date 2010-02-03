using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Deserialization {
    [TestClass]
    public class Dictionaries {
        [TestMethod, TestCategory("Deserialization"), TestCategory("Dictionary")]
        public void DeserializesEmptyDictionary() {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<IDictionary<string, int>>("{}");
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod, TestCategory("Deserialization"), TestCategory("Dictionary")]
        public void DeserializesOneItemDictionary() {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<IDictionary<string, int>>("{\"A\": 1}");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.ContainsKey("A"));
            Assert.AreEqual(1, result["A"]);
        }

        [TestMethod, TestCategory("Deserialization"), TestCategory("Dictionary")]
        public void DeserializesTwoItemDictionary() {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<IDictionary<string, int>>("{\"A\": 1, \"B\": 2}");
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.ContainsKey("A"));
            Assert.AreEqual(1, result["A"]);
            Assert.IsTrue(result.ContainsKey("B"));
            Assert.AreEqual(2, result["B"]);
        }
    }
}
