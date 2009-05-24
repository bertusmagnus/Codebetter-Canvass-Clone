namespace CodeBetter.Canvas.Helpers
{
    using System;

    public static class IntegerExtensions
    {
        public static void Times(this int count, Action<int> action)
        {
            for (var i = 1; i <= count; ++i)
            {
                action(i);
            }
        }
    }
}