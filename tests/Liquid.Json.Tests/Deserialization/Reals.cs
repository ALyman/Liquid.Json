using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Deserialization {
    [TestClass]
    public class Reals {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void SerializesSingle() {
            var serializer = new JsonSerializer();
            Assert.AreEqual<Single>(0f, serializer.Deserialize<Single>("0"));
            Assert.AreEqual<Single>(1f, serializer.Deserialize<Single>("1"));
            Assert.AreEqual<Single>(-1f, serializer.Deserialize<Single>("-1"));
            Assert.AreEqual<Single>(1.5f, serializer.Deserialize<Single>("1.5"));
            Assert.AreEqual<Single>(-1.5f, serializer.Deserialize<Single>("-1.5"));
        }

        [TestMethod]
        public void SerializesDouble() {
            var serializer = new JsonSerializer();
            Assert.AreEqual<Double>(0, serializer.Deserialize<Double>("0"));
            Assert.AreEqual<Double>(1, serializer.Deserialize<Double>("1"));
            Assert.AreEqual<Double>(-1, serializer.Deserialize<Double>("-1"));
            Assert.AreEqual<Double>(1.5, serializer.Deserialize<Double>("1.5"));
            Assert.AreEqual<Double>(-1.5, serializer.Deserialize<Double>("-1.5"));
        }

        [TestMethod]
        public void SerializesDecimal() {
            var serializer = new JsonSerializer();
            Assert.AreEqual<Decimal>(0m, serializer.Deserialize<Decimal>("0"));
            Assert.AreEqual<Decimal>(1m, serializer.Deserialize<Decimal>("1"));
            Assert.AreEqual<Decimal>(-1m, serializer.Deserialize<Decimal>("-1"));
            Assert.AreEqual<Decimal>(1.5m, serializer.Deserialize<Decimal>("1.5"));
            Assert.AreEqual<Decimal>(-1.5m, serializer.Deserialize<Decimal>("-1.5"));
        }
    }
}
