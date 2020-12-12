using System;
using System.Collections.Generic;
using System.Text;

namespace CubedApi.CustomExceptions
{
    public class NoDataException : Exception
    {
        public NoDataException()
        {
        }

        public NoDataException(string message) : base(message)
        {
        }

        public NoDataException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
