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
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<SByte>(0));
            Assert.AreEqual("1", serializer.Serialize<SByte>(1));
            Assert.AreEqual("-1", serializer.Serialize<SByte>(-1));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesInt16()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<Int16>(0));
            Assert.AreEqual("1", serializer.Serialize<Int16>(1));
            Assert.AreEqual("-1", serializer.Serialize<Int16>(-1));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesInt32()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize(0));
            Assert.AreEqual("1", serializer.Serialize(1));
            Assert.AreEqual("-1", serializer.Serialize(-1));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesInt64()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<Int64>(0));
            Assert.AreEqual("1", serializer.Serialize<Int64>(1));
            Assert.AreEqual("-1", serializer.Serialize<Int64>(-1));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesByte()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<Byte>(0));
            Assert.AreEqual("1", serializer.Serialize<Byte>(1));
            Assert.AreEqual("255", serializer.Serialize(Byte.MaxValue));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesUInt16()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<UInt16>(0));
            Assert.AreEqual("1", serializer.Serialize<UInt16>(1));
            Assert.AreEqual("65535", serializer.Serialize(UInt16.MaxValue));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesUInt32()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<UInt32>(0));
            Assert.AreEqual("1", serializer.Serialize<UInt32>(1));
            Assert.AreEqual("4294967295", serializer.Serialize(UInt32.MaxValue));
        }

        [TestMethod, TestCategory("Serialization")]
        public void SerializesUInt64()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<UInt64>(0));
            Assert.AreEqual("1", serializer.Serialize<UInt64>(1));
            Assert.AreEqual("18446744073709551615", serializer.Serialize(UInt64.MaxValue));
        }
    }
}