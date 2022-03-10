using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomThings
{
    public static class RandomStringExtensions
    {
        public static void InsertRandomStrings(this IList<string> source, int length = 10, int leftBorder = IndexesUTF8.LittleCyrillicA, int rightBorder = IndexesUTF8.BigCyrillicYa)
        {
            RandomString randomString = new RandomString();
            for (int i = 0; i < source.Count; i++)
            {
                source[i] = randomString.GetString();
            }
        }
    }
}
