using System;

namespace SquidLeagueWebsite.CustomExceptions
{
    public class ApiAccessException : Exception
    {
        public ApiAccessException()
        {
        }

        public ApiAccessException(string message) : base(message)
        {
        }

        public ApiAccessException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
