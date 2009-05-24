namespace CodeBetter.Canvas.Helpers
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
    }
}