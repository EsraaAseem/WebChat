using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using WebChat.Domain.Shared;

namespace WebChat.Presentation.MiddleWare
{
        public class GlobalExceptionHandlingMiddleware
        {
            private readonly RequestDelegate _next;

            public GlobalExceptionHandlingMiddleware(RequestDelegate next)
            {
                _next = next;
            }
            public async Task Invoke(HttpContext httpContext)
            {
                try
                {
                    await _next(httpContext);

                    if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                    {

                        var response = new BaseResponse();
                        response.IsSuccess = false;
                        response.Message = "User not Sign in";
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        httpContext.Response.ContentType = "application/json";
                        httpContext.Response.StatusCode = (int)HttpStatusCode.OK;

                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        };
                        var exceptionResult = JsonSerializer.Serialize(response, options);


                        await httpContext.Response.WriteAsync(exceptionResult);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    await HandlingExceptionsAsync(httpContext, ex);
                }
            }
            private static Task HandlingExceptionsAsync(HttpContext httpContext, Exception ex)
            {
                var response = new BaseResponse();
                response.IsSuccess = false;
                var exceptionType = ex.GetType();
               
                    response.Message = ex.Message;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var exceptionResult = JsonSerializer.Serialize(response, options);

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                return httpContext.Response.WriteAsync(exceptionResult);
            }
        }
    
}
