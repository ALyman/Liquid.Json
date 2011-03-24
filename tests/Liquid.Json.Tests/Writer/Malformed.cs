using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liquid.Json.Tests.Writer
{
    [TestClass]
    public class Malformed
    {
        [TestMethod, TestCategory("Writer")]
        public void TwoValuesAtRoot()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteValue(1);
            try {
                writer.WriteValue(2);
                Assert.Fail("Did not throw the expected exception");
            } catch (NotSupportedException ex) {
                Assert.AreEqual("Can not have two values at the root", ex.Message);
            }
        }

        [TestMethod, TestCategory("Writer")]
        public void UnnamedObjectProperty()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            writer.WriteStartObject();
            try {
                writer.WriteValue(1);
                Assert.Fail("Did not throw the expected exception");
            } catch (NotSupportedException ex) {
                Assert.AreEqual("Object properties must have a name written first", ex.Message);
            }
        }

        [TestMethod, TestCategory("Writer")]
        public void NameWithoutObject()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            try {
                writer.WriteName("X");
                Assert.Fail("Did not throw the expected exception");
            } catch (NotSupportedException ex) {
                Assert.AreEqual("Must be inside an object to write a name", ex.Message);
            }
        }

        [TestMethod, TestCategory("Writer")]
        public void EndOutsideObject()
        {
            var sw = new StringWriter();
            var writer = new JsonWriter(sw);
            try {
                writer.WriteEnd();
                Assert.Fail("Did not throw the expected exception");
            } catch (NotSupportedException ex) {
                Assert.AreEqual("Need to be inside an Array, Object or Constructor to End", ex.Message);
            }
        }
    }
}