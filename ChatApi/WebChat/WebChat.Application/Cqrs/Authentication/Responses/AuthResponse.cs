

namespace WebChat.Application.Cqrs.Authentication.Responses
{
    public class AuthResponse
    {
        public string Id { get; set; }
        public string? message { get; set; }

        public bool isAuthenticated { get; set; }
        
        public string UserName { get; set; }
        public DateTime ExpiresOn { get; set; }
        public string Token { get; set; }
        public string imgUrl { get; set; }
    }
}
