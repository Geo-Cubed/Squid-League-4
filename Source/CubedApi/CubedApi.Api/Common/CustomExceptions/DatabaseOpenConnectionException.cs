using System;

namespace CubedApi.Api.Common.CustomExceptions
{
    public class DatabaseOpenConnectionException : Exception
    {
        public DatabaseOpenConnectionException()
        {
        }

        public DatabaseOpenConnectionException(string message) : base(message)
        {
        }

        public DatabaseOpenConnectionException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
