using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace UnitTestingAsync
{
    public class RandomCollections
    {
        public static int[] GetInt(int count = 10, int bottomBorder = -10, int topBorder = 10)
        {
            int[] array = new int[count];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = RandomNumberGenerator.GetInt32(bottomBorder, topBorder);
            }
            return array;
        }
        public static async IAsyncEnumerable<int> GetIntEnumAsync(int count = 10, int bottomBorder = -10, int topBorder = 10)
        {
            for (int i = 0; i < count; i++)
            {
                await Task.Delay(1);
                yield return RandomNumberGenerator.GetInt32(bottomBorder, topBorder); 
            }
        }
    }
}
