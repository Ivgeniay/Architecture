using System;

namespace Utilits.Extensions
{
    public static class ExceptionExtensions
    {
        public static void ThrowIfNull(this System.Object target)
        {
            if (target is null)
                throw new ArgumentNullException(nameof(target));
        }

        public static void ThrowIfNullOrWhiteSpace(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentNullException($"{nameof(source)} is null or white space");
        }

        public static void ThrowIfNotContaint(this string source, string substring)
        {
            if (!source.Contains(substring))
                throw new ArgumentException($"{nameof(source)} is not contains {substring}");
        }
    }
}
