using NUnit.Framework;
using System;
using TypingBook.Extensions;

namespace Tests.Extensions
{
    class DateTimeExtensionsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ToReportStartDate1()
        {
            var testTime = new DateTime(1987, 01, 27, 11, 54, 12);
            var expected = new DateTime(1987, 01, 27, 00, 00, 00);

            var result = testTime.ToReportStartDate();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToReportEndDateDate1()
        {
            var testTime = new DateTime(1987, 01, 27, 11, 54, 12);
            var expected = new DateTime(1987, 01, 28, 00, 00, 00);

            var result = testTime.ToReportEndDate();

            Assert.AreEqual(expected, result);
        }
    }
}
