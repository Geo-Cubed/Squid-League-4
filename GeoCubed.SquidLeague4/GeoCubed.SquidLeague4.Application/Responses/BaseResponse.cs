using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Responses
{
    public class BaseResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public List<string> ValidationErrors { get; set; }

        public BaseResponse()
        {
            this.Success = true;
        }

        public BaseResponse(string message = null)
        {
            this.Success = true;
            this.Message = message;
        }

        public BaseResponse(string message, bool success)
        {
            this.Success = success;
            this.Message = message;
        }
    }
}
