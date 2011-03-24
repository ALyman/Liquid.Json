using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Deserialization
{
    [TestClass]
    public class Objects
    {
        [TestMethod, TestCategory("Deserialization")]
        public void EmptyObject()
        {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<EmptyObject_Class>("{}");
            Assert.IsInstanceOfType(result, typeof(EmptyObject_Class));
        }

        [TestMethod, TestCategory("Deserialization")]
        public void ObjectWithProperties()
        {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<ObjectWithProperties_Class>("{\"A\": 1, \"B\": 2, \"C\": 3}");
            Assert.IsInstanceOfType(result, typeof(ObjectWithProperties_Class));
            Assert.AreEqual(1, result.A);
            Assert.AreEqual(2, result.B);
            Assert.AreEqual(3, result.C);
        }
    }
}