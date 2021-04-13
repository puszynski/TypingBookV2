using System;
using System.Collections.Generic;
using System.Text.Json;

namespace TypingBookLibrary.Typing
{
    public static class BookPageSerializer
    {
        public static List<string> GetBookPages(string bookPagesString)
        {
            return JsonSerializer.Deserialize<List<string>>(bookPagesString);
        }

        public static string GetBookPagesString()
        {
            throw new NotImplementedException();
        }
    }
}
