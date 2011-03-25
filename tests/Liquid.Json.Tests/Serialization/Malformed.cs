using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Liquid.Json.TypeSerializers;

namespace Liquid.Json.Tests.Serialization
{
    [TestClass]
    public class Malformed
    {
        [TestMethod]
        public void IntegerSerializerFailsWithNonIntegralTypes()
        {
            var serializer = new JsonSerializer();
            var sw = new StringWriter();
            var context = new JsonSerializationContext(serializer, new JsonWriter(sw));
            var intSerializer = new JsonIntegerSerializer<float>();

            try {
                intSerializer.Serialize(0, context);
                Assert.Fail("Did not throw the expected exception");
            } catch (NotSupportedException) { }
        }

        [TestMethod]
        public void RealSerializerFailsWithNonRealTypes()
        {
            var serializer = new JsonSerializer();
            var sw = new StringWriter();
            var context = new JsonSerializationContext(serializer, new JsonWriter(sw));
            var intSerializer = new JsonRealSerializer<int>();

            try {
                intSerializer.Serialize(0, context);
                Assert.Fail("Did not throw the expected exception");
            } catch (NotSupportedException) { }
        }
    }
}
