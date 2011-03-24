using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Deserialization
{
    [TestClass]
    public class Integers
    {
        public TestContext TestContext { get; set; }

        [TestMethod, TestCategory("Deserialization")]
        public void SerializesSByte()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual<SByte>(0, serializer.Deserialize<SByte>("0"));
            Assert.AreEqual<SByte>(1, serializer.Deserialize<SByte>("1"));
            Assert.AreEqual<SByte>(-1, serializer.Deserialize<SByte>("-1"));
        }

        [TestMethod, TestCategory("Deserialization")]
        public void SerializesInt16()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual<Int16>(0, serializer.Deserialize<Int16>("0"));
            Assert.AreEqual<Int16>(1, serializer.Deserialize<Int16>("1"));
            Assert.AreEqual<Int16>(-1, serializer.Deserialize<Int16>("-1"));
        }

        [TestMethod, TestCategory("Deserialization")]
        public void SerializesInt32()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual(0, serializer.Deserialize<Int32>("0"));
            Assert.AreEqual(1, serializer.Deserialize<Int32>("1"));
            Assert.AreEqual(-1, serializer.Deserialize<Int32>("-1"));
        }

        [TestMethod, TestCategory("Deserialization")]
        public void SerializesInt64()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual(0, serializer.Deserialize<Int64>("0"));
            Assert.AreEqual(1, serializer.Deserialize<Int64>("1"));
            Assert.AreEqual(-1, serializer.Deserialize<Int64>("-1"));
        }

        [TestMethod, TestCategory("Deserialization")]
        public void SerializesByte()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual<Byte>(0, serializer.Deserialize<Byte>("0"));
            Assert.AreEqual<Byte>(1, serializer.Deserialize<Byte>("1"));
            Assert.AreEqual(Byte.MaxValue, serializer.Deserialize<Byte>("255"));
        }

        [TestMethod, TestCategory("Deserialization")]
        public void SerializesUInt16()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual<UInt16>(0, serializer.Deserialize<UInt16>("0"));
            Assert.AreEqual<UInt16>(1, serializer.Deserialize<UInt16>("1"));
            Assert.AreEqual(UInt16.MaxValue, serializer.Deserialize<UInt16>("65535"));
        }

        [TestMethod, TestCategory("Deserialization")]
        public void SerializesUInt32()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual<UInt32>(0, serializer.Deserialize<UInt32>("0"));
            Assert.AreEqual<UInt32>(1, serializer.Deserialize<UInt32>("1"));
            Assert.AreEqual(UInt32.MaxValue, serializer.Deserialize<UInt32>("4294967295"));
        }

        [TestMethod, TestCategory("Deserialization")]
        public void SerializesUInt64()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual<UInt64>(0, serializer.Deserialize<UInt64>("0"));
            Assert.AreEqual<UInt64>(1, serializer.Deserialize<UInt64>("1"));
            Assert.AreEqual(UInt64.MaxValue, serializer.Deserialize<UInt64>("18446744073709551615"));
        }
    }
}