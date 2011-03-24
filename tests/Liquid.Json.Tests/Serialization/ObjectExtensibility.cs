using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Liquid.Json.Tests.Serialization
{
    [TestClass]
    public class ObjectExtensibility
    {
        [TestMethod, TestCategory("Serialization")]
        public void TestMethod1()
        {
            JsonObjectSerializerFactory<X> factory = new JsonObjectSerializerFactory<X>()
                .WithMember(x => x.B)
                .WithMember(x => x.C);
            var serializer = new JsonSerializer(factory);
            var xM = new Mock<X>(MockBehavior.Strict);
            xM.SetupGet(x => x.B).Returns(123).Verifiable();
            xM.SetupGet(x => x.C).Returns(456).Verifiable();
            string result = serializer.Serialize(xM.Object);
            Assert.AreEqual("{\"B\": 123, \"C\": 456}", result);
            xM.VerifyAll();
        }

        #region Nested type: X

        public interface X
        {
            int A { get; set; }
            int B { get; set; }
            int C { get; set; }
        }

        #endregion
    }
}