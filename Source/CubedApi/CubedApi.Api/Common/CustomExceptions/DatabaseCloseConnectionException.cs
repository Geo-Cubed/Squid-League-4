using System;

namespace CubedApi.Api.Common.CustomExceptions
{
    public class DatabaseCloseConnectionException : Exception
    {
        public DatabaseCloseConnectionException()
        {
        }

        public DatabaseCloseConnectionException(string message) : base(message)
        {
        }

        public DatabaseCloseConnectionException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
