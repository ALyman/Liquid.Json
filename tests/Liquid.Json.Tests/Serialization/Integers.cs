using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Serialization
{
    [TestClass]
    public class Integers
    {
        public TestContext TestContext { get; set; }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesSByte()
        {
            Assert.AreEqual("0", Json.ToJson<SByte>(0));
            Assert.AreEqual("1", Json.ToJson<SByte>(1));
            Assert.AreEqual("-1", Json.ToJson<SByte>(-1));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesInt16()
        {
            Assert.AreEqual("0", Json.ToJson<Int16>(0));
            Assert.AreEqual("1", Json.ToJson<Int16>(1));
            Assert.AreEqual("-1", Json.ToJson<Int16>(-1));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesInt32()
        {
            Assert.AreEqual("0", Json.ToJson(0));
            Assert.AreEqual("1", Json.ToJson(1));
            Assert.AreEqual("-1", Json.ToJson(-1));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesInt64()
        {
            Assert.AreEqual("0", Json.ToJson<Int64>(0));
            Assert.AreEqual("1", Json.ToJson<Int64>(1));
            Assert.AreEqual("-1", Json.ToJson<Int64>(-1));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesByte()
        {
            Assert.AreEqual("0", Json.ToJson<Byte>(0));
            Assert.AreEqual("1", Json.ToJson<Byte>(1));
            Assert.AreEqual("255", Json.ToJson(Byte.MaxValue));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesUInt16()
        {
            Assert.AreEqual("0", Json.ToJson<UInt16>(0));
            Assert.AreEqual("1", Json.ToJson<UInt16>(1));
            Assert.AreEqual("65535", Json.ToJson(UInt16.MaxValue));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesUInt32()
        {
            Assert.AreEqual("0", Json.ToJson<UInt32>(0));
            Assert.AreEqual("1", Json.ToJson<UInt32>(1));
            Assert.AreEqual("4294967295", Json.ToJson(UInt32.MaxValue));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesUInt64()
        {
            Assert.AreEqual("0", Json.ToJson<UInt64>(0));
            Assert.AreEqual("1", Json.ToJson<UInt64>(1));
            Assert.AreEqual("18446744073709551615", Json.ToJson(UInt64.MaxValue));
        }
    }
}