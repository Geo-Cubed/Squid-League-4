using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> ValidationErrors { get; set; }

        public ValidationException(ValidationResult result)
        {
            this.ValidationErrors = new List<string>();
            foreach (var error in result.Errors)
            {
                this.ValidationErrors.Add(error.ErrorMessage);
            }
        }
    }
}
