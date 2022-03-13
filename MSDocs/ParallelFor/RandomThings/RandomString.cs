using System;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace RandomThings
{
    public class RandomString
    {
        public string GetString(int length = 10, int leftBorder = IndexesUTF8.LittleCyrillicA, int rightBorder = IndexesUTF8.BigCyrillicYa)
        {
            if (length < 0)
            {
                throw new ArgumentException("Length can't be negative number!");
            }
            if (leftBorder > rightBorder)
            {
                throw new ArgumentException("Invalid arguments! Left border can't be more than right border!");
            }

            char[] chars = new char[length];
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = (char)RandomNumberGenerator.GetInt32(leftBorder, rightBorder + 1);
            }
            return new string(chars);
        }
    }
}
