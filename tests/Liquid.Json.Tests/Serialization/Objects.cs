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
        class EmptyObject_Class { }

        [TestMethod]
        public void ObjectWithProperties() {
            var serializer = new JsonSerializer();
            var result = serializer.Serialize(new ObjectWithProperties_Class { A = 1, B = 2, C = 3 });
            Assert.AreEqual("{\"A\": 1, \"B\": 2, \"C\": 3}", result);
        }
        class ObjectWithProperties_Class {
            public int A { get; set; }
            public int B { get; set; }
            public int C { get; set; }
        }

        [TestMethod]
        public void ObjectWithFields() {
            var serializer = new JsonSerializer();
            var result = serializer.Serialize(new ObjectWithFields_Class { A = 1, B = 2, C = 3 });
            Assert.AreEqual("{\"A\": 1, \"B\": 2, \"C\": 3}", result);
        }
        class ObjectWithFields_Class {
            public int A;
            public int B;
            public int C;
        }

        [TestMethod]
        public void ObjectWithProperties_And_Fields() {
            var serializer = new JsonSerializer();
            var result = serializer.Serialize(new ObjectWithProperties_And_Fields_Class { A = 1, B = 2, C = 3 });
            Assert.AreEqual("{\"A\": 1, \"B\": 2, \"C\": 3}", result);
        }
        class ObjectWithProperties_And_Fields_Class {
            public int A { get; set; }
            public int B { get; set; }
            public int C;
        }

        [TestMethod]
        public void RespectsIgnoreAttributeOnProperties() {
            var serializer = new JsonSerializer();
            var result = serializer.Serialize(new RespectsIgnoreAttributeOnProperties_Class { A = 1, B = 2, C = 3 });
            Assert.AreEqual("{\"A\": 1, \"B\": 2}", result);
        }
        class RespectsIgnoreAttributeOnProperties_Class {
            public int A { get; set; }
            public int B { get; set; }
            [JsonIgnore]
            public int C { get; set; }
        }

        [TestMethod]
        public void RespectsIgnoreAttributeOnFields() {
            var serializer = new JsonSerializer();
            var result = serializer.Serialize(new RespectsIgnoreAttributeOnFields_Class { A = 1, B = 2, C = 3 });
            Assert.AreEqual("{\"A\": 1, \"B\": 2}", result);
        }
        class RespectsIgnoreAttributeOnFields_Class {
            public int A;
            public int B;
            [JsonIgnore]
            public int C;
        }
    }
}
