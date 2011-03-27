using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;

namespace Liquid.Json.Tests.Serialization
{
    [TestClass]
    public class ObjectExtensibility
    {
        [TestMethod, TestCategory("Serialization"), TestCategory("Extensibility")]
        public void SmokeTest()
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

        [TestMethod, TestCategory("Serialization"), TestCategory("Extensibility")]
        public void WithMemberFailsWithInvalidArguments()
        {
            var factory = new JsonObjectSerializerFactory<X>();
            try {
                factory.WithMember<int>(null);
                Assert.Fail("Did not throw");
            } catch (ArgumentNullException ex) { Assert.AreEqual("specifier", ex.ParamName); }

            try {
                factory.WithMember(x => x.ToString());
            } catch (NotSupportedException ex) {
                Assert.AreEqual("Can not serialize members that are not properties or fields", ex.Message);
            }

            try {
                X y = new Y();
                factory.WithMember(x => y.A);
            } catch (NotSupportedException ex) {
                Assert.AreEqual("Can not serialize members that are not declared directly on the target object", ex.Message);
            }
        }

        #region Nested type: X

        public interface X
        {
            int A { get; set; }

            int B { get; set; }

            int C { get; set; }
        }

        public class Y : X
        {

            public int A
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public int B
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public int C
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }
        }

        #endregion
    }
}