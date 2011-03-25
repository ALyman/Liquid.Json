using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Liquid.Json.Tests.Deserialization
{
    [TestClass]
    public class Other
    {
        [TestMethod, TestCategory("Deserialization")]
        public void DeserializesTrue()
        {
            var serializer = new JsonSerializer();
            Assert.IsTrue(serializer.Deserialize<Boolean>("true"));
        }

        [TestMethod, TestCategory("Deserialization")]
        public void DeserializesFalse()
        {
            var serializer = new JsonSerializer();
            Assert.IsFalse(serializer.Deserialize<Boolean>("false"));
        }

        [TestMethod, TestCategory("Deserialization")]
        public void DeserializesString()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual("abc", serializer.Deserialize<string>("\"abc\""));
        }

        [TestMethod, TestCategory("Deserialization")]
        public void DeserializesNullable()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual(null, serializer.Deserialize<int?>("null"));
            Assert.AreEqual(123, serializer.Deserialize<int?>("123"));
        }
    }
}
