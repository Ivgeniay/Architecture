using System.Text;
using System;

namespace Utilits.Extensions
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this String input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));

            return input[0].ToString().ToUpper() + input.Substring(1);
        }

        /// <summary>
        /// Takes a string like dir3/dir2/dir1 and rewrites it to dir1/dir2/dir3
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string ReversePath(this String input)
        {
            input.ThrowIfNullOrWhiteSpace();
            if (!input.Contains("/"))
                return input;

            StringBuilder result = new StringBuilder();
            var arr = input.Split("/");

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                result.Append(arr[i]);
                if (i != 0) result.Append("/");

            }
            return result.ToString();
        }
    }
}
