using System.Net;


namespace WebChat.Domain.Shared
{
    public class BaseResponse
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public bool IsSuccess { get; set; } = true;
        public object? Data { get; set; }
        public string Message { get; set; } = "";
        public async static Task<BaseResponse> SuccessResponseWithDataAndMsgAsync(object data,string message)
        {
            var response = new BaseResponse()
            {
                Data = data,
                Message = message
            };

            return response;
        }
        public async static Task<BaseResponse> SuccessResponseWithDataAsync(object data)
        {
            var response = new BaseResponse()
            {
                Data = data,
            };

            return response;
        }
        public async static Task<BaseResponse> SuccessResponseWithMessageAsync(string message)
        {
            var response = new BaseResponse()
            {
                Message = message
            };

            return response;
        }
        public async static Task<BaseResponse> FailedResponseAsync(HttpStatusCode statusCode,string message)
        {
            var response = new BaseResponse()
            {
               StatusCode = statusCode,
               IsSuccess = false,
               Message = message
            };

            return response;
        }
        public async static Task<BaseResponse> NotFoundResponsAsync(string message)
        {
            var response = new BaseResponse()
            {
                StatusCode = HttpStatusCode.NotFound,
                IsSuccess = false,
                Message = message
            };

            return response;
        }
        public async static Task<BaseResponse> BadRequestResponsAsync(string message)
        {
            var response = new BaseResponse()
            {
                StatusCode = HttpStatusCode.BadRequest,
                IsSuccess = false,
                Message = message
            };

            return response;
        }
    }
}
