using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilits.Extensions
{
    internal static class CommonExtensions
    {
        /// <summary>
        /// Return true if object is not exist
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool NotExist(this System.Object target) =>
            target == null;

        /// <summary>
        /// Return true if object is exist
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool Exist(this System.Object target) =>
            target != null;


        public static string FirstCharToUpper(this String input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));

            return input[0].ToString().ToUpper() + input.Substring(1);
        }


    }
}
