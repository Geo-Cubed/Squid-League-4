using System;
using System.Collections.Generic;
using System.Text;

namespace CubedApi.CustomExceptions
{
    public class InvalidIdException : Exception
    {
        public InvalidIdException()
        {
        }

        public InvalidIdException(string message) : base(message)
        {
        }

        public InvalidIdException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
