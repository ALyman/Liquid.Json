using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Liquid.Json.Tests.Serialization
{
    [TestClass]
    public class Extensibility
    {
        public TestContext TestContext { get; set; }

        [TestMethod, TestCategory("Serialization")]
        public void UsesFactories()
        {
            var factory = new Mock<IJsonTypeSerializerFactory>(MockBehavior.Strict);
            var typeSerializer = new Mock<IJsonTypeSerializer<Int32>>(MockBehavior.Loose);
            var serializer = new JsonSerializer(factory.Object);
            factory
                .Setup(f => f.CreateSerializer<Int32>(serializer))
                .Returns(typeSerializer.Object)
                .Verifiable("Didn't get the serializer from the factory")
                ;
            serializer.Serialize(0);
            factory.VerifyAll();
        }

        [TestMethod, TestCategory("Serialization")]
        public void UsesCustomSerializer()
        {
            var factory = new Mock<IJsonTypeSerializerFactory>(MockBehavior.Loose);
            var typeSerializer = new Mock<IJsonTypeSerializer<Int32>>(MockBehavior.Strict);
            var serializer = new JsonSerializer(factory.Object);
            factory
                .Setup(f => f.CreateSerializer<Int32>(serializer))
                .Returns(typeSerializer.Object)
                ;
            typeSerializer
                .Setup(s => s.Serialize(0, It.IsAny<JsonSerializationContext>()))
                .Callback((int @object, JsonSerializationContext context) => {
                              Assert.AreEqual(0, @object);
                              context.Writer.WriteValue(456);
                          }).Verifiable("Didn't get the serializer from the factory")
                ;
            Assert.AreEqual("456", serializer.Serialize(0));
            typeSerializer.VerifyAll();
        }
    }
}