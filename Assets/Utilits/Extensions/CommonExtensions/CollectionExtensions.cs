using System;
using System.Collections.Generic;

namespace Utilits.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> FilterCollection<T>(this IEnumerable<T> collection, T coincidence, Func<T, T, bool> predicate) {
            List<T> list = new List<T>();
            foreach (T item in collection)  {
                if (predicate(item, coincidence))
                    list.Add(item);
            }

            return list;
        }
    }
}
