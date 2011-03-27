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
            Assert.AreEqual("0", Json.ToJson(0f));
            Assert.AreEqual("1", Json.ToJson(1f));
            Assert.AreEqual("-1", Json.ToJson(-1f));
            Assert.AreEqual("1.5", Json.ToJson(1.5f));
            Assert.AreEqual("-1.5", Json.ToJson(-1.5f));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesDouble()
        {
            Assert.AreEqual("0", Json.ToJson<Double>(0));
            Assert.AreEqual("1", Json.ToJson<Double>(1));
            Assert.AreEqual("-1", Json.ToJson<Double>(-1));
            Assert.AreEqual("1.5", Json.ToJson(1.5));
            Assert.AreEqual("-1.5", Json.ToJson(-1.5));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesDecimal()
        {
            Assert.AreEqual("0", Json.ToJson(0m));
            Assert.AreEqual("1", Json.ToJson(1m));
            Assert.AreEqual("-1", Json.ToJson(-1m));
            Assert.AreEqual("1.5", Json.ToJson(1.5m));
            Assert.AreEqual("-1.5", Json.ToJson(-1.5m));
        }
    }
}