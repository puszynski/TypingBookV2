using NUnit.Framework;
using System.Collections.Generic;
using TypingBook.Extensions;

namespace Tests.Extensions
{
    class EnumExtensionsTest
    {
        [Test]
        public void ToStrTest()
        {
            var result = TestEnum.TestEnumJeden.ToStr();
            Assert.AreEqual("TestEnumJeden", result);
        }

        [Test]
        public void ConvertEnumSumToIntArrayTest()
        {
            var enumSum = (int)TestEnum.TestEnumJeden + (int)TestEnum.TestEnumCztery;

            var result = enumSum.ConvertEnumSumToIntArray();
            var expectedResult = new List<int>() { 1, 8 };

            Assert.AreEqual(expectedResult, result);
        }
    }

    enum TestEnum
    {
        TestEnumJeden=1,
        TestEnumDwa=2,
        TestEnumTrzy=4,
        TestEnumCztery=8
    };
}
