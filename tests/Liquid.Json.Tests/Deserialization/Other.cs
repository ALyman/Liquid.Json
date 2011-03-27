using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Liquid.Json.Tests.Deserialization
{
    [TestClass]
    public class Other
    {
        [TestMethod, TestCategory("Deserialization")]
        public void DeserializesTrue()
        {
            var serializer = new JsonSerializer();
            Assert.IsTrue(serializer.Deserialize<Boolean>("true"));
        }

        [TestMethod, TestCategory("Deserialization")]
        public void DeserializesFalse()
        {
            var serializer = new JsonSerializer();
            Assert.IsFalse(serializer.Deserialize<Boolean>("false"));
        }

        [TestMethod, TestCategory("Deserialization")]
        public void DeserializesString()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual("abc", serializer.Deserialize<string>("\"abc\""));
        }

        [TestMethod, TestCategory("Deserialization")]
        public void DeserializesNullable()
        {
            var serializer = new JsonSerializer();
            Assert.AreEqual(null, serializer.Deserialize<int?>("null"));
            Assert.AreEqual(123, serializer.Deserialize<int?>("123"));
        }

        [TestMethod, TestCategory("Deserialization")]
        public void Date()
        {
            var serializer = new JsonSerializer();
            DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            Action<DateTime, string> CheckDate = (date, json) => {
                var result = serializer.Deserialize<DateTime>(json);
                var diff = date.Subtract(result);
                Assert.IsTrue(diff.TotalSeconds < 1, string.Format("Dates do not match: <{0}> != <{1}>, difference: {2}, json: <{3}>", date, result, diff.TotalSeconds, json));
            };

            Action<DateTime> TestDate = (source) => {
                CheckDate(source, string.Format("new Date({0})", (long)(source - EPOCH).TotalMilliseconds));
                CheckDate(source, string.Format("new Date(\"{0}\")", source.ToString("MMMM d, yyyy HH:mm:ss zzz")));
                CheckDate(source, string.Format("new Date({0}, {1}, {2}, {3}, {4}, {5}, {6})", source.Year, source.Month, source.Day, source.Hour, source.Minute, source.Second, source.Millisecond));
            };

            TestDate(new DateTime(1970, 1, 2, 0, 0, 0, DateTimeKind.Utc));
            TestDate(new DateTime(1970, 1, 2, 4, 0, 0, DateTimeKind.Utc));
            TestDate(new DateTime(1970, 1, 2, 14, 0, 0, DateTimeKind.Utc));
            TestDate(DateTime.Now);
            TestDate(DateTime.UtcNow);

            CheckDate(new DateTime(1970, 1, 2, 4, 1, 2), "new Date(1970, 1, 2, 14, 0, 0)");
            CheckDate(new DateTime(1970, 1, 2, 4, 1, 0), "new Date(1970, 1, 2, 14, 0)");
            CheckDate(new DateTime(1970, 1, 2, 4, 0, 0), "new Date(1970, 1, 2, 14)");
            CheckDate(new DateTime(1970, 1, 2, 0, 0, 0), "new Date(1970, 1, 2)");
        }
    }
}
