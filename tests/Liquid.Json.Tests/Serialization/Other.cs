using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Serialization {
    [TestClass]
    public class Other {
        public TestContext TestContext { get; set; }

        [TestMethod, TestCategory("Serialization")]
        public void Date() {
            var serializer = new JsonSerializer();

            Assert.AreEqual("new Date(86400)", serializer.Serialize(new DateTime(1970, 1, 2, 0, 0, 0, DateTimeKind.Utc)));
        }

        [TestMethod, TestCategory("Serialization")]
        public void Boolean_True() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("true", serializer.Serialize(true));
        }

        [TestMethod, TestCategory("Serialization")]
        public void Boolean_False() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("false", serializer.Serialize(false));
        }

        [TestMethod, TestCategory("Serialization")]
        public void ValuedNullable() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<int?>(0));
        }

        [TestMethod, TestCategory("Serialization")]
        public void NullNullable() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("null", serializer.Serialize<int?>(null));
        }
    }
}
