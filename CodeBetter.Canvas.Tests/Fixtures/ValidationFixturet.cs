namespace CodeBetter.Canvas.Tests.ValidatorTests
{
    using System;
    using System.Reflection;
    using Validation;

    public class ValidationFixture : BaseFixture, IDisposable
    {    
        private bool _disposed;

        public ValidationFixture()
        {
            ValidatorConfiguration.Initialize("CodeBetter.Canvas.Tests");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (_disposed || !disposing)
            {
                return;
            }
            var field = typeof(ValidatorConfiguration).GetField("_rules", BindingFlags.NonPublic | BindingFlags.Static);
            field.SetValue(null, null);
            _disposed = true;
        }
    }
}