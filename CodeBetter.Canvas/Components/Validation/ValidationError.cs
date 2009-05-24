namespace CodeBetter.Canvas.Validation
{
    public class ValidationError
    {
        public string Field { get; private set; }
        public string Message { get; private set; }

        public ValidationError(string field)
        {
            Field = field;
        }

        public ValidationError(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
}