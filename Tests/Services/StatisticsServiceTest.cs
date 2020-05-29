using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TypingBook.Repositories.IReporitories;
using TypingBook.Services;

namespace Tests.Services
{
    class StatisticsServiceTest
    {
        IUserDataRepository _userDataRepository;

        [SetUp]
        public void SetUp()
        {
            _userDataRepository = new Mock<IUserDataRepository>().Object;

            var mock = new Mock<IUserDataRepository>();
            Expression<Func<IUserDataRepository, string>> experssion = x => x.GetStatisticsByUserId(It.IsAny<string>());

            var mockUserData = JsonConvert.SerializeObject(new List<(DateTime month, int typedCrrect, int typedWrong, int secondsOfTyping)>
            {
                (new DateTime(2020,01,01), 3450, 230, 230 ),
                (new DateTime(2020,02,01), 3450, 220, 220 ),
                (new DateTime(2020,03,01), 3450, 200, 200 )
            });

            mock.Setup(experssion).Returns(mockUserData);

            _userDataRepository = mock.Object;
        }

        [Test]
        // https://rubikscode.net/2018/04/16/implementing-and-testing-repository-pattern-using-entity-framework/
        public void SaveDataByUserIdTest()
        {
            var statisticsService = new StatisticsService(_userDataRepository);
            statisticsService.SaveDataByUserId("", 100, 20, 10);

            Assert.AreEqual(mockUserData, result);
        }

        [Test]
        public void GetUserDataByIdTest()
        {
            var statisticsService = new StatisticsService(_userDataRepository);
            var result = statisticsService.GetUserDataById("");

            var mockUserData = new List<(DateTime month, int typedCrrect, int typedWrong, int secondsOfTyping)>
            {
                (new DateTime(2020,01,01), 3450, 230, 230 ),
                (new DateTime(2020,02,01), 3450, 220, 220 ),
                (new DateTime(2020,03,01), 3450, 200, 200 )
            };

            Assert.AreEqual(mockUserData, result);
        }
    }
}
