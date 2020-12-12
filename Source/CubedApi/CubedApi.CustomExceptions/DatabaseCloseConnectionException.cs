using System;
using System.Collections.Generic;
using System.Text;

namespace CubedApi.CustomExceptions
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
