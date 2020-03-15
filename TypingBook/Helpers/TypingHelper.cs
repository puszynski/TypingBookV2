using System.Collections.Generic;
using System.Text.RegularExpressions;
using TypingBook.Extensions;

namespace TypingBook.Helpers
{
    public class TypingHelper
    {
        #region Singleton
        private static TypingHelper _typingHelper;

        private TypingHelper()
        {
        }

        public static TypingHelper GetInstance()
        {
            if (_typingHelper == null)
                _typingHelper = new TypingHelper();
            return _typingHelper;
        }
        #endregion

        #region DivideBook
        
        #endregion DivideBook        
    }
}
