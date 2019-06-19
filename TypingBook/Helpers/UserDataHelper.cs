using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingBook.Helpers
{
    public class UserDataHelper
    {
        public Dictionary<int, int> DeserializeProgressBar(string input)
        {
            var result = new Dictionary<int, int>();

            var inputList = input.Split(' ');

            foreach (var item in inputList)
            {
                var temp = item.Split(':');
                result.TryAdd(Int32.Parse(temp[0]), Int32.Parse(temp[1]));
            }

            return result;
        }

        public string SerializeProgressBar(Dictionary<int, int> input)
        {
            var sb = new StringBuilder();

            foreach (var item in input)
            {
                sb.Append(item.Key+":"+item.Value+" ");
            }

            return sb.ToString();
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
