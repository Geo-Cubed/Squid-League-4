using System;

namespace CubedApi.Api.Common.CustomExceptions
{
    public class DataIsNullException : Exception
    {
        public DataIsNullException()
        {
        }

        public DataIsNullException(string message) : base(message)
        {
        }

        public DataIsNullException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
