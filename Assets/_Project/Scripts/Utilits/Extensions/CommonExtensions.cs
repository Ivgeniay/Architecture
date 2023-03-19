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




        public static T With<T>(this T self, Action<T> @do)
        {
            @do.Invoke(self);
            return self;
        }

        public static T With<T>(this T self, Action<T> apply, Func<bool> @if)
        {
            if (@if())
                apply.Invoke(self);
            return self;
        }

        public static T With<T>(this T self, Action<T> apply, Func<T, bool> @if)
        {
            if (@if(self))
                apply.Invoke(self);
            return self;
        }

        public static T With<T>(this T self, Action<T> apply, bool @if)
        {
            if (@if)
                apply.Invoke(self);
            return self;
        }
    }
}
