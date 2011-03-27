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
        public void ObjectInPlace()
        {
            var result = new ObjectWithProperties_Class();
            var serializer = new JsonSerializer();
            serializer.DeserializeInto(ref result, "{\"A\": 1, \"B\": 2, \"C\": 3}");
            Assert.IsInstanceOfType(result, typeof(ObjectWithProperties_Class));
            Assert.AreEqual(1, result.A);
            Assert.AreEqual(2, result.B);
            Assert.AreEqual(3, result.C);
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

        [TestMethod, TestCategory("Deserialization")]
        public void ObjectWithInvalidProperty()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<ObjectWithProperties_Class>("{\"X\": 1}");
            } catch (JsonDeserializationException ex) {
                Assert.AreEqual("Property 'X' not found on type Liquid.Json.Tests.ObjectWithProperties_Class", ex.Message);
            }
        }

        [TestMethod, TestCategory("Deserialization")]
        public void ObjectWithReadOnlyChild()
        {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<ObjectWithReadOnlyChild_Class>("{A: {\"A\": 1, \"B\": 2, \"C\": 3}}");
            Assert.IsInstanceOfType(result, typeof(ObjectWithReadOnlyChild_Class));
            Assert.AreEqual(1, result.A.A);
            Assert.AreEqual(2, result.A.B);
            Assert.AreEqual(3, result.A.C);
        }

        [TestMethod, TestCategory("Deserialization")]
        public void ObjectWithReadOnlyChildFailsOnBadInput()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<ObjectWithReadOnlyChild_Class>("{A: {\"A\": 1, \"B\": \"X\", \"C\": 3}}");
                Assert.Fail("Did not throw the expected exception");
            } catch (JsonUnexpectedTokenException ex) {
                Assert.AreEqual("Unexpected token '\"X\"', expected: integer", ex.Message);
            }
        }

        [TestMethod, TestCategory("Deserialization")]
        public void ObjectWithNullReadOnlyChildFails()
        {
            var serializer = new JsonSerializer();
            try {
                var result =
                    serializer.Deserialize<ObjectWithNullReadOnlyChild_Class>("{A: {\"A\": 1, \"B\": 2, \"C\": 3}}");
                Assert.Fail("Did not throw the expected exception");
            } catch (ArgumentNullException ex) {
                Assert.AreEqual("@object", ex.ParamName);
            }
        }
    }
}