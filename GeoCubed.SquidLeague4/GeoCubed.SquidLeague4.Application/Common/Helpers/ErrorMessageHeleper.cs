namespace GeoCubed.SquidLeague4.Application.Common.Helpers
{
    public static class ErrorMessageHeleper
    {
        public static string GetNullArguementMessage(object argumentType, object className)
        {
            return string.Format("Cannot have a null [{0}] in class [{1}]", argumentType, className);
        }
    }
}
