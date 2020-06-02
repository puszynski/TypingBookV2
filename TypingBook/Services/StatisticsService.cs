using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TypingBook.Repositories.IReporitories;
using TypingBook.Services.IServices;

namespace TypingBook.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUserDataRepository _userDataRepository;

        public StatisticsService(IUserDataRepository userDataRepository) 
            => (_userDataRepository) = (userDataRepository);

        public List<(DateTime month, int typedCrrect, int typedWrong, int secondsOfTyping)> GetUserDataById(string userId)
        {
            var stringData = _userDataRepository.GetStatisticsByUserId(userId);            
            return JsonConvert.DeserializeObject<List<(DateTime month, int typedCrrect, int typedWrong, int secondsOfTyping)>>(stringData);
        }

        public void SaveDataByUserId(string userId, int typedCorrect, int typedWrong, int secondsOfTyping)
        {
            var stringData = _userDataRepository.GetStatisticsByUserId(userId);
            List<(DateTime month, int typedCrrect, int typedWrong, int secondsOfTyping)> userData;

            var actuallMonthDate = new DateTime(DateTime.UtcNow.Year, DateTime.Now.Month, 1);

            if (string.IsNullOrEmpty(stringData))
                userData = new List<(DateTime month, int typedCorrect, int typedWrong, int secondsOfTyping)>
                {
                    (actuallMonthDate, 0, 0, 0)
                };
            else
                userData = JsonConvert.DeserializeObject<List<(DateTime month, int typedCorrect, int typedWrong, int secondsOfTyping)>>(stringData);

            var userDataRow = userData.FindIndex(x => x.month == actuallMonthDate);
            var selectedDataRow = userData.Single(x => x.month == actuallMonthDate);
            
            userData[userDataRow] = (actuallMonthDate, 
                                     selectedDataRow.typedCrrect + typedCorrect,
                                     selectedDataRow.typedWrong + typedWrong,
                                     selectedDataRow.secondsOfTyping + secondsOfTyping);

            _userDataRepository.UpateStatisticsByUserId(userId, JsonConvert.SerializeObject(userData));
            _userDataRepository.SaveChanges();
        }
    }
}
