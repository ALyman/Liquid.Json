using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Serialization {
    [TestClass]
    public class Objects {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void EmptyObject() {
            var serializer = new JsonSerializer();
            var result = serializer.Serialize(new EmptyObject_Class());
            Assert.AreEqual("{}", result);
        }

        [TestMethod]
        public void ObjectWithProperties() {
            var serializer = new JsonSerializer();
            var result = serializer.Serialize(new ObjectWithProperties_Class { A = 1, B = 2, C = 3 });
            Assert.AreEqual("{\"A\": 1, \"B\": 2, \"C\": 3}", result);
        }

        [TestMethod]
        public void ObjectWithFields() {
            var serializer = new JsonSerializer();
            var result = serializer.Serialize(new ObjectWithFields_Class { A = 1, B = 2, C = 3 });
            Assert.AreEqual("{\"A\": 1, \"B\": 2, \"C\": 3}", result);
        }

        [TestMethod]
        public void ObjectWithProperties_And_Fields() {
            var serializer = new JsonSerializer();
            var result = serializer.Serialize(new ObjectWithProperties_And_Fields_Class { A = 1, B = 2, C = 3 });
            Assert.AreEqual("{\"A\": 1, \"B\": 2, \"C\": 3}", result);
        }

        [TestMethod]
        public void RespectsIgnoreAttributeOnProperties() {
            var serializer = new JsonSerializer();
            var result = serializer.Serialize(new RespectsIgnoreAttributeOnProperties_Class { A = 1, B = 2, C = 3 });
            Assert.AreEqual("{\"A\": 1, \"B\": 2}", result);
        }

        [TestMethod]
        public void RespectsIgnoreAttributeOnFields() {
            var serializer = new JsonSerializer();
            var result = serializer.Serialize(new RespectsIgnoreAttributeOnFields_Class { A = 1, B = 2, C = 3 });
            Assert.AreEqual("{\"A\": 1, \"B\": 2}", result);
        }

        [TestMethod]
        public void NullObject() {
            var serializer = new JsonSerializer();
            var result = serializer.Serialize<RespectsIgnoreAttributeOnFields_Class>(null);
            Assert.AreEqual("null", result);
        }
    }
}
