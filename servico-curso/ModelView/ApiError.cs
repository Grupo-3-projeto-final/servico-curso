using System;
using System.Net;

namespace servico_curso.Controller
{
    public class ApiError 
    {
        public HttpStatusCode StatusCode { get; private set; }
        public string MessageError { get; private set; }

        public ApiError(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            MessageError = message;
        }
        public ApiError(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }

}