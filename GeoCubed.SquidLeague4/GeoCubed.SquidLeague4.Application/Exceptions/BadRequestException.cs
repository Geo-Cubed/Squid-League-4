using System;

namespace GeoCubed.SquidLeague4.Application.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message) : base(message) 
        {
        }
    }
}
