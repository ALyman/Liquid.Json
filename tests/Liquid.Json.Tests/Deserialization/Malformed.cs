using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Liquid.Json.Tests.Deserialization
{
    [TestClass]
    public class Malformed
    {
        public TestContext TestContext { get; set; }

        [TestMethod, TestCategory("Deserialization")]
        public void ObjectWithInvalidPropertyName()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<ObjectWithArrays_Class>("{new: [1, 2, 3], \"B\": [4, 5, 6]}");
            } catch (JsonUnexpectedTokenException ex) {
                Assert.AreEqual("Unexpected token 'new', expected: string, identifier", ex.Message);
            }
        }

        [TestMethod, TestCategory("Deserialization"), Ignore]
        public void UnclosedArray()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<ObjectWithArrays_Class>("{A: [1}");
            } catch (JsonUnexpectedTokenException ex) {
                Assert.AreEqual("Unexpected token 'new', expected: string, identifier", ex.Message);
            }
        }
    }
}
