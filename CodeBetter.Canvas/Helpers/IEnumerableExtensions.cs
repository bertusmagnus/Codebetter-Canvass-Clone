namespace CodeBetter.Canvas
{
    using System;
    using System.Collections.Generic;

    public static class IEnumerableExtensions
    {
        public static void Each<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach(var l in list)
            {
                action(l);
            }
        }
        public static T[] Map<T>(this IEnumerable<T> list, Func<T, bool> action)
        {
            var newList = new List<T>();
            foreach (var l in list)
            {
                if (action(l))
                {
                    newList.Add(l);
                }                
            }
            return newList.ToArray();
        }
        public static K[] Map<T, K>(this IEnumerable<T> list, Func<T, K> action)
        {
            var newList = new List<K>();
            foreach (var l in list)
            {
                var k = action(l);
                if (k != null)
                {
                    newList.Add(k);
                }
            }
            return newList.ToArray();
        }
    }
}