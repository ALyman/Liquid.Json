using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Liquid.Json.Tests
{
    [TestClass]
    public class MemberUtilsTests
    {
        [TestMethod]
        public void SetMemberValueFailsWithMethodMember()
        {
            var x = new ObjectWithProperties_And_Fields_Class();
            try {
                MemberUtils.SetMemberValue(
                    typeof(ObjectWithProperties_And_Fields_Class).GetMethod("X",
                                                                            BindingFlags.Public | BindingFlags.Instance),
                    x,
                    null);
            } catch (ArgumentException ex) {
                Assert.AreEqual("member", ex.ParamName);
                Assert.IsTrue(ex.Message.StartsWith("Member type not supported: Method"), "No match: <" + ex.Message + ">");
            }
        }

        [TestMethod]
        public void IsReadOnlyFailsWithMethodMember()
        {
            var x = new ObjectWithProperties_And_Fields_Class();
            try {
                MemberUtils.IsReadOnly(
                    typeof(ObjectWithProperties_And_Fields_Class).GetMethod("X",
                                                                            BindingFlags.Public | BindingFlags.Instance));
            } catch (ArgumentException ex) {
                Assert.AreEqual("member", ex.ParamName);
                Assert.IsTrue(ex.Message.StartsWith("Member type not supported: Method"), "No match: <" + ex.Message + ">");
            }
        }

        [TestMethod]
        public void GetMemberTypeFailsWithMethodMember()
        {
            var x = new ObjectWithProperties_And_Fields_Class();
            try {
                MemberUtils.GetMemberType(
                    typeof(ObjectWithProperties_And_Fields_Class).GetMethod("X",
                                                                            BindingFlags.Public | BindingFlags.Instance));
            } catch (ArgumentException ex) {
                Assert.AreEqual("member", ex.ParamName);
                Assert.IsTrue(ex.Message.StartsWith("Member type not supported: Method"), "No match: <" + ex.Message + ">");
            }
        }
        [TestMethod]
        public void GetMemberValueFailsWithMethodMember()
        {
            var x = new ObjectWithProperties_And_Fields_Class();
            try {
                MemberUtils.GetMemberValue(
                    typeof(ObjectWithProperties_And_Fields_Class).GetMethod("X",
                                                                            BindingFlags.Public | BindingFlags.Instance), x);
            } catch (ArgumentException ex) {
                Assert.AreEqual("member", ex.ParamName);
                Assert.IsTrue(ex.Message.StartsWith("Member type not supported: Method"), "No match: <" + ex.Message + ">");
            }
        }
    }
}
