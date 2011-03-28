using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;

namespace Liquid.Json.Tests.Deserialization
{
    [TestClass]
    public class Streams
    {
        [TestMethod, TestCategory("Deserialization"), TestCategory("Streams")]
        public void DeserializesStream()
        {
            var serializer = new JsonSerializer();
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream)) {
                writer.Write("123");
                writer.Flush();
                stream.Position = 0;
                Assert.AreEqual<Int16>(123, serializer.Deserialize<Int16>(stream));
            }
        }


        [TestMethod, TestCategory("Deserialization"), TestCategory("Streams")]
        public void DeserializeInPlace()
        {
            var result = new ObjectWithProperties_Class();
            var serializer = new JsonSerializer();
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream)) {
                writer.Write("{\"A\": 1, \"B\": 2, \"C\": 3}");
                writer.Flush();
                stream.Position = 0;
                serializer.DeserializeInto(ref result, stream);
                Assert.IsInstanceOfType(result, typeof(ObjectWithProperties_Class));
                Assert.AreEqual(1, result.A);
                Assert.AreEqual(2, result.B);
                Assert.AreEqual(3, result.C);
            }
        }
    }
}