using System;
using System.Collections.Generic;
using System.Text;

namespace CubedApi.CustomExceptions
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
