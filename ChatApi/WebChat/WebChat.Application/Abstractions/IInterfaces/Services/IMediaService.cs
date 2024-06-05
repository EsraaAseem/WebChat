

using Microsoft.AspNetCore.Http;

namespace WebChat.Application.Abstractions.IInterfaces.Services
{
    public interface IMediaService
    {
        Task<string> UploadImageAsync(IFormFile file, string folderName);
    }
}
