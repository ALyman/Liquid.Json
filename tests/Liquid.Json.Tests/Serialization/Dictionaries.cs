using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Liquid.Json.Tests.Serialization {
    [TestClass]
    public class Dictionaries {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void StringDictionary() {
            var serializer = new JsonSerializer();
            var dict = new Dictionary<string, string> {
                {"a","b"},
                {"c","d"},
                {"e","f"}
            };
            Assert.AreEqual("{\"a\": \"b\", \"c\": \"d\", \"e\": \"f\"}", serializer.Serialize(dict));
        }

        [TestMethod]
        public void SerializesHashtable() {
            var serializer = new JsonSerializer();
            var dict = new Hashtable {
                {"a","b"},
                {"c","d"},
                {"e","f"}
            };
            Assert.AreEqual("{\"a\": \"b\", \"c\": \"d\", \"e\": \"f\"}", serializer.Serialize(dict));
        }

        [TestMethod]
        public void IntDictionary() {
            var serializer = new JsonSerializer();
            var dict = new Dictionary<int, int> {
                {1,2},{3,4},{5,6}
            };
            Assert.AreEqual("{\"1\": 2, \"3\": 4, \"5\": 6}", serializer.Serialize(dict));
        }

        [TestMethod]
        public void NullDictionary() {
            var serializer = new JsonSerializer();
            var result = serializer.Serialize<IDictionary<string, string>>(null);
            Assert.AreEqual("null", result);
        }
    }
}
