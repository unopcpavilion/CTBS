using System.ComponentModel.DataAnnotations;
using System.Net;
using CTBS.API.Core.Exceptions;

namespace CTBS.API.Middlewares;

public class HttpStatusCodeInfo
{
    public HttpStatusCode Code { get; }
    public string Message { get; }

    public HttpStatusCodeInfo(HttpStatusCode code, string message)
    {
        Code = code;
        Message = message;
    }
}

public static class ExceptionToHttpStatusMapper
{
    public static Func<Exception, HttpStatusCode>? CustomMap { get; set; }

    public static HttpStatusCodeInfo Map(Exception exception)
    {
        var code = exception switch
        {
            UnauthorizedAccessException _ => HttpStatusCode.Unauthorized,
            NotImplementedException _ => HttpStatusCode.NotImplemented,
            InvalidOperationException _ => HttpStatusCode.Conflict,
            ArgumentException _ => HttpStatusCode.BadRequest,
            ValidationException _ => HttpStatusCode.BadRequest,
            EntityNotFoundException _ => HttpStatusCode.NotFound,
            _ => CustomMap?.Invoke(exception) ?? HttpStatusCode.InternalServerError
        };

        return new HttpStatusCodeInfo(code, exception.Message);
    }
}