using System;
using System.Collections.Generic;

namespace TypingBook.Services.IServices
{
    public interface IStatisticService
    {
        List<(DateTime month, int typedCrrect, int typedWrong, int secondsOfTyping)> GetUserDataById(string userId);
        void SaveDataByUserId(string userId, int typedCorrect, int typedWrong, int secondsOfTyping);
    }
}
