using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Serialization
{
    [TestClass]
    public class Streams
    {
        [TestMethod, TestCategory("Serializaton"), TestCategory("Streams")]
        public void Serializes()
        {
            using (var stream = new MemoryStream()) {
                var serializer = new JsonSerializer();
                serializer.Serialize(new[] { 0, 1, 2 }, stream);
                stream.Position = 0;
                using (var reader = new StreamReader(stream)) {
                    Assert.AreEqual("[0, 1, 2]", reader.ReadToEnd());
                }
            }
        }

        [TestMethod, TestCategory("Serializaton"), TestCategory("Streams")]
        public void SerializeAs()
        {
            using (var stream = new MemoryStream()) {
                var serializer = new JsonSerializer();
                serializer.SerializeAs(typeof(int[]), new[] { 0, 1, 2 }, stream);
                stream.Position = 0;
                using (var reader = new StreamReader(stream)) {
                    Assert.AreEqual("[0, 1, 2]", reader.ReadToEnd());
                }
            }
        }
    }
}