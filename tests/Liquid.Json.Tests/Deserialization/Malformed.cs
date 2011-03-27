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

        [TestMethod, TestCategory("Deserialization")]
        public void ObjectWithNoPropertyName()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<ObjectWithArrays_Class>("{: [1, 2, 3], \"B\": [4, 5, 6]}");
            } catch (JsonUnexpectedTokenException ex) {
                Assert.AreEqual("Unexpected token ':', expected: string, identifier", ex.Message);
            }
        }

        [TestMethod, TestCategory("Deserialization")]
        public void DictionaryWithInvalidPropertyName()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<IDictionary<string, int>>("{new: 1, \"B\": 2}");
            } catch (JsonUnexpectedTokenException ex) {
                Assert.AreEqual("Unexpected token 'new', expected: string, identifier", ex.Message);
            }
        }

        [TestMethod, TestCategory("Deserialization")]
        public void DictionaryWithNoPropertyName()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<IDictionary<string, int>>("{: 1, \"B\": 2}");
            } catch (JsonUnexpectedTokenException ex) {
                Assert.AreEqual("Unexpected token ':', expected: string, identifier", ex.Message);
            }
        }
        [TestMethod, TestCategory("Deserialization")]
        public void UnclosedArray()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<int[]>("[1}");
            } catch (JsonUnexpectedTokenException ex) {
                Assert.AreEqual("Unexpected token '}', expected: ']', ','", ex.Message);
            }
        }

        [TestMethod, TestCategory("Deserialization")]
        public void UnclosedList()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<IList<int>>("[1}");
            } catch (JsonUnexpectedTokenException ex) {
                Assert.AreEqual("Unexpected token '}', expected: ']', ','", ex.Message);
            }
        }

        [TestMethod, TestCategory("Deserialization")]
        public void UnclosedObject()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<ObjectWithFields_Class>("{A: 123]");
            } catch (JsonUnexpectedTokenException ex) {
                Assert.AreEqual("Unexpected token ']', expected: '}', ','", ex.Message);
            }
        }

        [TestMethod, TestCategory("Deserialization")]
        public void UnclosedDictionary()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<IDictionary<string, int>>("{A: 123]");
            } catch (JsonUnexpectedTokenException ex) {
                Assert.AreEqual("Unexpected token ']', expected: '}', ','", ex.Message);
            }
        }

        [TestMethod, TestCategory("Deserialization")]
        public void UnclosedArrayAtEOF()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<int[]>("[1");
            } catch (JsonUnexpectedEndOfStreamException ex) {
                Assert.AreEqual("Unexpected end of file, expected: ']', ','", ex.Message);
            }
        }

        [TestMethod, TestCategory("Deserialization")]
        public void UnclosedListAtEOF()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<List<int>>("[1");
            } catch (JsonUnexpectedEndOfStreamException ex) {
                Assert.AreEqual("Unexpected end of file, expected: ']', ','", ex.Message);
            }
        }

        [TestMethod, TestCategory("Deserialization")]
        public void UnclosedObjectAtEOF()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<ObjectWithFields_Class>("{A: 123");
            } catch (JsonUnexpectedEndOfStreamException ex) {
                Assert.AreEqual("Unexpected end of file, expected: '}', ','", ex.Message);
            }
        }

        [TestMethod, TestCategory("Deserialization")]
        public void UnclosedDictionaryAtEOF()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<IDictionary<string, int>>("{A: 123");
            } catch (JsonUnexpectedEndOfStreamException ex) {
                Assert.AreEqual("Unexpected end of file, expected: '}', ','", ex.Message);
            }
        }

        [TestMethod, TestCategory("Deserialization")]
        public void ExpectedNullableAtEOF()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<ObjectWithNullable_Class>("{A: ");
            } catch (JsonUnexpectedEndOfStreamException ex) {
                Assert.AreEqual("Unexpected end of file", ex.Message);
            }
        }

        [TestMethod, TestCategory("Deserialization")]
        public void ExpectedNewDate()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<ObjectWithDate_Class>("{A: 1}");
            } catch (JsonUnexpectedTokenException ex) {
                Assert.AreEqual("Unexpected token '1', expected: 'new'", ex.Message);
            }
        }

        [TestMethod, TestCategory("Deserialization")]
        public void UnexpectedDateTypeName()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<ObjectWithDate_Class>("{A: new BLAH(1970, 1, 2)}");
            } catch (JsonUnexpectedTokenException ex) {
                Assert.AreEqual("Unexpected token 'BLAH', expected: 'Date'", ex.Message);
            }
        }

        [TestMethod, TestCategory("Deserialization")]
        public void ExpectedDateAtEOF()
        {
            var serializer = new JsonSerializer();
            try {
                var result = serializer.Deserialize<ObjectWithDate_Class>("{A: ");
            } catch (JsonUnexpectedEndOfStreamException ex) {
                Assert.AreEqual("Unexpected end of file, expected: 'new'", ex.Message);
            }
        }
    }
}
