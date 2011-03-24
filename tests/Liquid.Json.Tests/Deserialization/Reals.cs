using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Deserialization
{
    [TestClass]
    public class Reals
    {
        public TestContext TestContext { get; set; }

        [TestMethod, TestCategory("Deserialization")]
        public void SerializesSingle()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual(0f, serializer.Deserialize<Single>("0"));
            Assert.AreEqual(1f, serializer.Deserialize<Single>("1"));
            Assert.AreEqual(-1f, serializer.Deserialize<Single>("-1"));
            Assert.AreEqual(1.5f, serializer.Deserialize<Single>("1.5"));
            Assert.AreEqual(-1.5f, serializer.Deserialize<Single>("-1.5"));
        }

        [TestMethod, TestCategory("Deserialization")]
        public void SerializesDouble()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual(0, serializer.Deserialize<Double>("0"));
            Assert.AreEqual(1, serializer.Deserialize<Double>("1"));
            Assert.AreEqual(-1, serializer.Deserialize<Double>("-1"));
            Assert.AreEqual(1.5, serializer.Deserialize<Double>("1.5"));
            Assert.AreEqual(-1.5, serializer.Deserialize<Double>("-1.5"));
        }

        [TestMethod, TestCategory("Deserialization")]
        public void SerializesDecimal()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual(0m, serializer.Deserialize<Decimal>("0"));
            Assert.AreEqual(1m, serializer.Deserialize<Decimal>("1"));
            Assert.AreEqual(-1m, serializer.Deserialize<Decimal>("-1"));
            Assert.AreEqual(1.5m, serializer.Deserialize<Decimal>("1.5"));
            Assert.AreEqual(-1.5m, serializer.Deserialize<Decimal>("-1.5"));
        }
    }
}