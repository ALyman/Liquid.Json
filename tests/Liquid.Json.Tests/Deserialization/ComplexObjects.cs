using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Deserialization
{
    [TestClass]
    public class ComplexObjects
    {
        [TestMethod, TestCategory("Deserialization")]
        public void ArrayOfObjects()
        {
            var serializer = new JsonSerializer();
            var result =
                serializer.Deserialize<List<ObjectWithProperties_Class>>(
                    "[{\"A\": 1, \"B\": 2, \"C\": 3}, {\"A\": 4, \"B\": 5, \"C\": 6}, {\"A\": 7, \"B\": 8, \"C\": 9}]");
            var expected = new[] {
                new ObjectWithProperties_Class { A = 1, B = 2, C = 3 },
                new ObjectWithProperties_Class { A = 4, B = 5, C = 6 },
                new ObjectWithProperties_Class { A = 7, B = 8, C = 9 }
            };
            CollectionAssert.AreEqual(expected, result, Comparer<ObjectWithProperties_Class>.Default);
        }

        [TestMethod, TestCategory("Deserialization")]
        public void ObjectWithArrays()
        {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<ObjectWithArrays_Class>("{\"A\": [1, 2, 3], \"B\": [4, 5, 6]}");
            var expected = new ObjectWithArrays_Class {
                A = new[] { 1, 2, 3 },
                B = new[] { 4, 5, 6 }
            };

            Assert.IsTrue(EqualityComparer<ObjectWithArrays_Class>.Default.Equals(expected, result));
        }

        [TestMethod, TestCategory("Deserialization")]
        public void ObjectWithCaseInsensitiveProperties()
        {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize<ObjectWithCaseInsensitiveProperties_Class>("{\"ABC\": 1, \"abc\": 2}");
            var expected = new ObjectWithCaseInsensitiveProperties_Class {
                ABC = 1,
                abc = 2
            };

            Assert.IsTrue(EqualityComparer<ObjectWithCaseInsensitiveProperties_Class>.Default.Equals(expected, result));
        }

        [TestMethod, TestCategory("Deserialization")]
        public void ObjectWithBadPropertyCandidates()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<ObjectWithCaseInsensitiveProperties_Class>("{\"Abc\": 1}");
                Assert.Fail("Did not throw the expected exception");
            } catch (JsonDeserializationException ex) {
                Assert.AreEqual(
                    "Multiple candidate members for 'Abc' could not be resolved by case sensitivity; candidates were: 'ABC', 'abc'",
                    ex.Message);
            }
        }
    }
}