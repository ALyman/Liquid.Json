using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Deserialization
{
    [TestClass]
    public class Lists
    {
        [TestMethod, TestCategory("Deserialization"), TestCategory("Array")]
        public void DeserializesEmptyArray()
        {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<int[]>("[]");
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod, TestCategory("Deserialization"), TestCategory("Array")]
        public void DeserializesOneItemArray()
        {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<int[]>("[123]");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(123, result[0]);
        }

        [TestMethod, TestCategory("Deserialization"), TestCategory("Array")]
        public void DeserializesTwoItemArray()
        {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<int[]>("[123, 456]");
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(123, result[0]);
            Assert.AreEqual(456, result[1]);
        }

        [TestMethod, TestCategory("Deserialization"), TestCategory("List")]
        public void DeserializesEmptyList()
        {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<IList<int>>("[]");
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod, TestCategory("Deserialization"), TestCategory("List")]
        public void DeserializesOneItemList()
        {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<IList<int>>("[123]");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(123, result[0]);
        }

        [TestMethod, TestCategory("Deserialization"), TestCategory("List")]
        public void DeserializesTwoItemList()
        {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<IList<int>>("[123, 456]");
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(123, result[0]);
            Assert.AreEqual(456, result[1]);
        }

        [TestMethod, TestCategory("Deserialization"), TestCategory("Enumerable")]
        public void DeserializesEmptyEnumerable()
        {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<IEnumerable<int>>("[]");
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod, TestCategory("Deserialization"), TestCategory("Enumerable")]
        public void DeserializesOneItemEnumerable()
        {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<IEnumerable<int>>("[123]");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(123, result.Single());
        }

        [TestMethod, TestCategory("Deserialization"), TestCategory("Enumerable")]
        public void DeserializesTwoItemEnumerable()
        {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<IEnumerable<int>>("[123, 456]");
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(123, result.First());
            Assert.AreEqual(456, result.Skip(1).Single());
        }
    }
}