using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Serialization {
    [TestClass]
    public class Reals {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void SerializesSingle() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<Single>(0f));
            Assert.AreEqual("1", serializer.Serialize<Single>(1f));
            Assert.AreEqual("-1", serializer.Serialize<Single>(-1f));
            Assert.AreEqual("1.5", serializer.Serialize<Single>(1.5f));
            Assert.AreEqual("-1.5", serializer.Serialize<Single>(-1.5f));
        }

        [TestMethod]
        public void SerializesDouble() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<Double>(0));
            Assert.AreEqual("1", serializer.Serialize<Double>(1));
            Assert.AreEqual("-1", serializer.Serialize<Double>(-1));
            Assert.AreEqual("1.5", serializer.Serialize<Double>(1.5));
            Assert.AreEqual("-1.5", serializer.Serialize<Double>(-1.5));
        }

        [TestMethod]
        public void SerializesDecimal() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<Decimal>(0m));
            Assert.AreEqual("1", serializer.Serialize<Decimal>(1m));
            Assert.AreEqual("-1", serializer.Serialize<Decimal>(-1m));
            Assert.AreEqual("1.5", serializer.Serialize<Decimal>(1.5m));
            Assert.AreEqual("-1.5", serializer.Serialize<Decimal>(-1.5m));
        }
    }
}
