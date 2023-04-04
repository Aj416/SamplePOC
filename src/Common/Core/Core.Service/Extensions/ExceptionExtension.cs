namespace Core.Service.Extensions
{
    public static class ExceptionExtension
    {
        public static IEnumerable<Exception> WithInnerException(this Exception exception)
        {
            var ex = exception;
            while (ex != null)
            {
                yield return ex;
                ex = ex.InnerException;
            }
        }
    }
}
