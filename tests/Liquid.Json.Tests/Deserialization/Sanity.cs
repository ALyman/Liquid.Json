using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Deserialization
{
    [TestClass]
    public class Sanity
    {
        [TestMethod, TestCategory("Deserialization")]
        public void DeserializeInplaceTFailsWithNullTarget()
        {
            var serializer = new JsonSerializer();
            var reader = new JsonReader(new StringReader("X"));
            var context = new JsonDeserializationContext(serializer, reader);
            ObjectWithArrays_Class c = null;
            try {
                context.DeserializeInplace(ref c);
                Assert.Fail("Did not throw the expected exception.");
            } catch (ArgumentNullException ex) {
                Assert.AreEqual("@object", ex.ParamName);
            }
        }

        [TestMethod, TestCategory("Deserialization")]
        public void DeserializeInplaceT()
        {
            var serializer = new JsonSerializer();
            var reader = new JsonReader(new StringReader("{A: [1,2]}"));
            var context = new JsonDeserializationContext(serializer, reader);
            ObjectWithArrays_Class c = new ObjectWithArrays_Class();
            Assert.IsTrue(context.CanDeserializeInplace<ObjectWithArrays_Class>());
            context.DeserializeInplace(ref c);
            Assert.AreEqual(2, c.A.Length);
            Assert.AreEqual(1, c.A[0]);
            Assert.AreEqual(2, c.A[1]);
        }
    }
}
