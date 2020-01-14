using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypingBook.Helpers
{
    /// <summary>
    /// e.g. 2:35 4:12 7:188 => Dictionary<BookId,BookPage> => last saved book go first
    /// </summary>
    public class UserDataHelper
    {
        public int? GetLastBookId(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            var bookID = input.Split(':').First();

            return Int32.Parse(bookID);
        }

        public int? GetLastCurrentBookPage(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            var bookPage = input.Split(' ').First().Split(':')[1];

            return Int32.Parse(bookPage);
        }


        public Dictionary<int, int> DeserializeProgressBar(string input)
        {
            if (string.IsNullOrEmpty(input))
                return new Dictionary<int, int>(); 

            var result = new Dictionary<int, int>();

            var inputList = input.Split(' ');

            foreach (var item in inputList)
            {
                var temp = item.Split(':');
                result.TryAdd(Int32.Parse(temp[0]), Int32.Parse(temp[1]));
            }

            return result;
        }

        public int? GetCurrentBookPageByBookId(string input, int bookId)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            var bookPage = input
                            .Split(' ')
                            .Where(x => x.StartsWith(bookId.ToString()))
                            .FirstOrDefault();

            if (string.IsNullOrEmpty(bookPage))
                return null;

            return Int32.Parse(bookPage.Split(':')[1]);
        }

        public string SerializeProgressBar(Dictionary<int, int> input, int firstBookId)
        {
            var sb = new StringBuilder();

            // todo firsy => add appemd first
            var firstBookItem = input.First(x => x.Key == firstBookId);
            sb.Append(firstBookItem.Key + ":" + firstBookItem.Value + " ");
            input.Remove(firstBookId);

            foreach (var item in input)
            {
                sb.Append(item.Key + ":" + item.Value + " ");
            }

            return sb.ToString().TrimEnd();
        }

        public Dictionary<DateTime, int> DeserializeStatisticBar(string input)
        {
            var result = new Dictionary<DateTime, int>();

            var inputList = input.Split(' ');

            foreach (var item in inputList)
            {
                var temp = item.Split(':');
                result.TryAdd(DateTime.Parse(temp[0]), Int32.Parse(temp[1]));
            }

            return result;
        }

        public string SerializeProgressBar(Dictionary<DateTime, int> input)
        {
            var sb = new StringBuilder();

            foreach (var item in input)
            {
                sb.Append(item.Key + ":" + item.Value + " ");
            }

            return sb.ToString();
        }
    }
}
