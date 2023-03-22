using System;

namespace Utilits.Extensions
{
    public static class TypeConfiguringExtensions
    {

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
            else if (@elseif(self))
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

        //Simple if/else
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
