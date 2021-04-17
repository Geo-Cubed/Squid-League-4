using System;

namespace CubedApi.Api.Common.CustomExceptions
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
