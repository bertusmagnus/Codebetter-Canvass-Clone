namespace CodeBetter.Canvas.Helpers
{
    using System.Xml;

    public static class XmlNodeExtensions
    {
        public static string GetString(this XmlNode node, string attributeName)
        {
            return node.Attributes[attributeName].Value;
        }
    }
}
