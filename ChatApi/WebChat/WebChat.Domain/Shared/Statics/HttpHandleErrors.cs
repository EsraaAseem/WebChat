﻿using System.Net;

namespace WebChat.Domain.Shared.Statics
{
    public static class HttpHandleErrors
    {
        public const HttpStatusCode BadRequest = HttpStatusCode.BadRequest;
        public const HttpStatusCode Unauthorized = HttpStatusCode.Unauthorized;
        public const HttpStatusCode Forbidden = HttpStatusCode.Forbidden;
        public const HttpStatusCode NotFound = HttpStatusCode.NotFound;
        public const HttpStatusCode InternalServerError = HttpStatusCode.InternalServerError;
        public const HttpStatusCode NotImplemented = HttpStatusCode.NotImplemented;
        public const HttpStatusCode ServiceUnavailable = HttpStatusCode.ServiceUnavailable;
        public const HttpStatusCode NotExtended = HttpStatusCode.NotExtended;
    }
}
