namespace CodeBetter.Canvas
{
    using System.Text;

    public static class StringBuilderExtensions
    {
        public static StringBuilder RemoveLast(this StringBuilder sb)
        {
            return sb.RemoveLast(1);
        }
        public static StringBuilder RemoveLastIf(this StringBuilder sb, char c)
        {
            if (sb.Length > 0 && sb[sb.Length - 1] == c)
            {
                sb.RemoveLast();
            }
            return sb;            
        }
        public static StringBuilder RemoveLast(this StringBuilder sb, int count)
        {
            if (sb.Length > count)
            {
                sb.Remove(sb.Length - count, count);
            }
            return sb;
        }        

    }
}