using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Serialization
{
    [TestClass]
    public class Reals
    {
        public TestContext TestContext { get; set; }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesSingle()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize(0f));
            Assert.AreEqual("1", serializer.Serialize(1f));
            Assert.AreEqual("-1", serializer.Serialize(-1f));
            Assert.AreEqual("1.5", serializer.Serialize(1.5f));
            Assert.AreEqual("-1.5", serializer.Serialize(-1.5f));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesDouble()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<Double>(0));
            Assert.AreEqual("1", serializer.Serialize<Double>(1));
            Assert.AreEqual("-1", serializer.Serialize<Double>(-1));
            Assert.AreEqual("1.5", serializer.Serialize(1.5));
            Assert.AreEqual("-1.5", serializer.Serialize(-1.5));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesDecimal()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize(0m));
            Assert.AreEqual("1", serializer.Serialize(1m));
            Assert.AreEqual("-1", serializer.Serialize(-1m));
            Assert.AreEqual("1.5", serializer.Serialize(1.5m));
            Assert.AreEqual("-1.5", serializer.Serialize(-1.5m));
        }
    }
}