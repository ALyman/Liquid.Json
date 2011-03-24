using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Serialization
{
    [TestClass]
    public class ComplexObjects
    {
        [TestMethod, TestCategory("Serialization")]
        public void ArrayOfObjects()
        {
            var serializer = new JsonSerializer();
            string result = serializer.Serialize(
                new[] {
                    new ObjectWithProperties_Class { A = 1, B = 2, C = 3 },
                    new ObjectWithProperties_Class { A = 4, B = 5, C = 6 },
                    new ObjectWithProperties_Class { A = 7, B = 8, C = 9 }
                }
                );
            Assert.AreEqual(
                "[{\"A\": 1, \"B\": 2, \"C\": 3}, {\"A\": 4, \"B\": 5, \"C\": 6}, {\"A\": 7, \"B\": 8, \"C\": 9}]",
                result);
        }

        [TestMethod, TestCategory("Serialization")]
        public void ObjectWithArrays()
        {
            var serializer = new JsonSerializer();
            string result = serializer.Serialize(
                new ObjectWithArrays_Class {
                    A = new[] { 1, 2, 3 },
                    B = new[] { 4, 5, 6 }
                }
                );
            Assert.AreEqual("{\"A\": [1, 2, 3], \"B\": [4, 5, 6]}", result);
        }

        [TestMethod, TestCategory("Serialization"),
         ExpectedException(typeof(JsonSerializationException), "Did not detect cycle"), Timeout(1000)]
        public void CyclicalObject()
        {
            var serializer = new JsonSerializer();
            var a = new CyclicalObject_Class();
            a.A = a;
            string result = serializer.Serialize(a);
        }

        [TestMethod, TestCategory("Serialization"),
         ExpectedException(typeof(JsonSerializationException), "Did not detect cycle"), Timeout(1000)]
        public void CocyclicalObjects()
        {
            var serializer = new JsonSerializer();
            var a = new CyclicalObject_Class();
            var b = new CyclicalObject_Class2();
            a.B = b;
            b.A = a;
            string result = serializer.Serialize(a);
        }
    }
}