namespace CodeBetter.Canvas.Helpers
{
    public static class StringExtensions
    {
        public static int ToInt(this string @string, int defaultValue)
        {
            int value;
            return int.TryParse(@string, out value) ? value : defaultValue;
        }
    }
}