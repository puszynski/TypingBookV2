using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TypingBook.Repositories.IReporitories;
using TypingBook.Services.IServices;

namespace TypingBook.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IUserDataRepository _userDataRepository;

        public StatisticService(IUserDataRepository userDataRepository)
            => (_userDataRepository) = (userDataRepository);

        public List<(DateTime date, int typedCorrect, int typedWrong, int secondsOfTyping)> GetUserDataById(string userId)
        {
            var stringData = _userDataRepository.GetStatisticsByUserId(userId);
            return JsonConvert.DeserializeObject<List<(DateTime date, int typedCrrect, int typedWrong, int secondsOfTyping)>>(stringData);
        }

        public void SaveDataByUserId(string userId, int typedCorrect, int typedWrong, int millisecondsOfTyping)
        {
            var secondsOfTyping = (int)TimeSpan.FromMilliseconds(millisecondsOfTyping).TotalSeconds;

            var stringData = _userDataRepository.GetStatisticsByUserId(userId);
            List<(DateTime date, int typedCorrect, int typedWrong, int secondsOfTyping)> userData;

            var actuallMonthDate = new DateTime(DateTime.UtcNow.Year, DateTime.Now.Month, 1);

            if (string.IsNullOrEmpty(stringData))
                userData = new List<(DateTime date, int typedCorrect, int typedWrong, int secondsOfTyping)>
                {
                    (actuallMonthDate, 0, 0, 0)
                };
            else
                userData = JsonConvert.DeserializeObject<List<(DateTime date, int typedCorrect, int typedWrong, int secondsOfTyping)>>(stringData);
                        

            var userDataRow = userData.SingleOrDefault(x => x.date == actuallMonthDate);
            if (userDataRow.date == new DateTime())
            {
                userDataRow = (actuallMonthDate, 0, 0, 0);
                userData.Add(userDataRow);
            }

            var userDataRowIndex = userData.FindIndex(x => x.date == actuallMonthDate);
            var selectedDataRow = userData.Single(x => x.date == actuallMonthDate);

            userData[userDataRowIndex] = (actuallMonthDate,
                                     selectedDataRow.typedCorrect + typedCorrect,
                                     selectedDataRow.typedWrong + typedWrong,
                                     selectedDataRow.secondsOfTyping + secondsOfTyping);

            _userDataRepository.UpateStatisticsByUserId(userId, JsonConvert.SerializeObject(userData));
            _userDataRepository.SaveChanges();
        }        
    }
}
