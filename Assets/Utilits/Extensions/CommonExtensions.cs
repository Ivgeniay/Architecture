using System;
using System.Linq;
using System.Text;

namespace Utilits.Extensions
{
    public static class CommonExtensions
    {
        /// <summary>
        /// Return true if object is not exist
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsNotExist(this System.Object target) =>
            target == null;

        /// <summary>
        /// Return true if object is exist
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsExist(this System.Object target) =>
            target != null;


        //string
        public static string FirstCharToUpper(this String input) {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));

            return input[0].ToString().ToUpper() + input.Substring(1);
        }

        public static string ReversePath(this String input) {

            if (string.IsNullOrEmpty(input))
                throw new ArgumentException(nameof(input));

            if (!input.Contains("/"))
                return input;

            StringBuilder result = new StringBuilder();
            var arr = input.Split("/");
            
            for(int i = arr.Length - 1; i >= 0; i--)
            {
                result.Append(arr[i]);
                if (i != 0) result.Append("/");

            }

            return result.ToString();
        }





        public static T With<T>(this T self, Action<T> @do)
        {
            @do.Invoke(self);
            return self;
        }


        //Lambda if/else
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

        public static T With<T>(this T self, Action<T> apply, Func<T, bool> @if, Func<T, bool> @elseif)
        {
            if (@if(self))
                apply.Invoke(self);
            else if (@elseif (self))
                apply.Invoke(self);
            return self;
        }

        public static T With<T>(this T self, Action<T> apply, Func<T, bool> @if, Func<T, bool> @elseif, Action<T> @else)
        {
            if (@if(self))
                apply.Invoke(self);
            else if (@elseif(self))
                apply.Invoke(self);
            else
                @else.Invoke(self);
            
            return self;
        }

        //Simble if/else
        public static T With<T>(this T self, Action<T> apply, bool @if)
        {
            if (@if)
                apply.Invoke(self);
            return self;
        }

        public static T With<T>(this T self, Action<T> apply, bool @if, bool @elseif)
        {
            if (@if)
                apply.Invoke(self);
            else if (elseif)
                apply.Invoke(self);
            return self;
        }

        public static T With<T>(this T self, Action<T> apply, bool @if, bool @elseif, Action<T> @else)
        {
            if (@if)
                apply.Invoke(self);
            else if (elseif)
                apply.Invoke(self);
            else
                @else.Invoke(self);
            return self;
        }
    }
}
