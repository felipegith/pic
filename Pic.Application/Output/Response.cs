using System.Net;
using FluentValidation.Results;

namespace Pic.Application;

public class Response
{
    public Response(){}
    
    public Response(bool success, string message, HttpStatusCode statuscode, object content)
    {
        Success = success;
        Message = message;
        StatusCode = statuscode;
        Content = content;
    }

    public Response(bool success, string message, HttpStatusCode statuscode)
    {
        Success = success;
        Message = message;
        StatusCode = statuscode;
    }

    public Response(bool success, string message, HttpStatusCode statusCode, List<object> contents) : this(success, message, statusCode)
    {
        Contents = contents;
    }

    public Response(List<ValidationFailure> erros)
    {
        Erros = erros;
    }

    public bool Success { get; set; }
    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public object Content { get; set; }
    public List<object> Contents { get; set; }

    public List<ValidationFailure>? Erros { get; set; }
    
}
