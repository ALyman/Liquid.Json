using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Serialization
{
    [TestClass]
    public class Strings
    {
        public TestContext TestContext { get; set; }

        [TestMethod, TestCategory("Serialization")]
        public void Serializes_NonEscaped_String()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual("\"hello, world\"", serializer.Serialize("hello, world"));
        }

        [TestMethod, TestCategory("Serialization")]
        public void Escapes_Quote()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual("\"this is escaped: \\\"\"", serializer.Serialize("this is escaped: \""));
        }
    }
}