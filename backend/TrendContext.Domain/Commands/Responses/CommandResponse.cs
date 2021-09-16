using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendContext.Domain.Commands.Responses
{
    public class CommandResponse<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Payload { get; set; }

        public CommandResponse(bool success, int statusCode, string message, T payload)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
            Payload = payload;
        }
    }
}
