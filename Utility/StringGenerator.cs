using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkGroupImageParser.Utility;

public class StringGenerator
{
    private static Random random = new Random();
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}

public static class StringSpintax
{
    public static string Spintax(this string sourceStr)
    {
        char[] chars = { '|', '{', '}', '!', ']', '[', ' ', '/', '*', '\\', '#', '"', '\'' };

        StringBuilder sb = new StringBuilder();
        Random rand = new Random();

        string[] parts = sourceStr.Split(' ');

        foreach (var part in parts)
        {
            int randNumb = rand.Next(0, 1);
            char randChar = chars[rand.Next(0, chars.Length - 1)];
            if (randNumb == 0)
                sb.AppendJoin(' ', $" {randChar}{part}");
            else
                sb.AppendJoin(' ', $"{part}{randChar} ");

        }

        return sb.ToString();
    }
}

