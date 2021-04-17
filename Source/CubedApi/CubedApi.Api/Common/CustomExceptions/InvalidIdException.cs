using System;

namespace CubedApi.Api.Common.CustomExceptions
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
