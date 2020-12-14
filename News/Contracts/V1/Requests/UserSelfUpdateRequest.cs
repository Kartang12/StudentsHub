using System.ComponentModel.DataAnnotations;

namespace News.Contracts.V1.Requests
{
    public class UserSelfUpdateRequest
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}