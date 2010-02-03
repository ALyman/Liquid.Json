using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Deserialization {
    [TestClass]
    public class Arrays {
        [TestMethod, TestCategory("Deserialization"), TestCategory("Array")]
        public void DeserializesEmptyArray() {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<IList<int>>("[]");
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod, TestCategory("Deserialization"), TestCategory("Array")]
        public void DeserializesOneItemArray() {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<IList<int>>("[123]");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(123, result[0]);
        }

        [TestMethod, TestCategory("Deserialization"), TestCategory("Array")]
        public void DeserializesTwoItemArray() {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<IList<int>>("[123, 456]");
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(123, result[0]);
            Assert.AreEqual(456, result[1]);
        }
    }
}
