using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Liquid.Json.TypeSerializers;

namespace Liquid.Json.Tests.TypeSerializerFactory
{
    [TestClass]
    public class Factory
    {
        [TestMethod]
        public void FailsWithUnknownTypeCode()
        {
            try {
                new DefaultJsonTypeSerializerFactory().CreateSerializer<DBNull>(null);
                Assert.Fail("Did not throw the expected exception");
            } catch (NotSupportedException ex) {
                Assert.AreEqual("Invalid Type: System.DBNull", ex.Message);
            }
        }
    }
}
