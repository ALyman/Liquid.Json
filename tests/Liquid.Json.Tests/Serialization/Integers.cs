using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Liquid.Json.Tests.Serialization {
    [TestClass]
    public class Integers {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void SerializesSByte() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<SByte>(0));
            Assert.AreEqual("1", serializer.Serialize<SByte>(1));
            Assert.AreEqual("-1", serializer.Serialize<SByte>(-1));
        }

        [TestMethod]
        public void SerializesInt16() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<Int16>(0));
            Assert.AreEqual("1", serializer.Serialize<Int16>(1));
            Assert.AreEqual("-1", serializer.Serialize<Int16>(-1));
        }

        [TestMethod]
        public void SerializesInt32() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<Int32>(0));
            Assert.AreEqual("1", serializer.Serialize<Int32>(1));
            Assert.AreEqual("-1", serializer.Serialize<Int32>(-1));
        }

        [TestMethod]
        public void SerializesInt64() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<Int64>(0));
            Assert.AreEqual("1", serializer.Serialize<Int64>(1));
            Assert.AreEqual("-1", serializer.Serialize<Int64>(-1));
        }

        [TestMethod]
        public void SerializesByte() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<Byte>(0));
            Assert.AreEqual("1", serializer.Serialize<Byte>(1));
            Assert.AreEqual("255", serializer.Serialize<Byte>(Byte.MaxValue));
        }

        [TestMethod]
        public void SerializesUInt16() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<UInt16>(0));
            Assert.AreEqual("1", serializer.Serialize<UInt16>(1));
            Assert.AreEqual("65535", serializer.Serialize<UInt16>(UInt16.MaxValue));
        }

        [TestMethod]
        public void SerializesUInt32() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<UInt32>(0));
            Assert.AreEqual("1", serializer.Serialize<UInt32>(1));
            Assert.AreEqual("4294967295", serializer.Serialize<UInt32>(UInt32.MaxValue));
        }

        [TestMethod]
        public void SerializesUInt64() {
            var serializer = new JsonSerializer();
            Assert.AreEqual("0", serializer.Serialize<UInt64>(0));
            Assert.AreEqual("1", serializer.Serialize<UInt64>(1));
            Assert.AreEqual("18446744073709551615", serializer.Serialize<UInt64>(UInt64.MaxValue));
        }
    }
}
