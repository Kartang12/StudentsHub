using System.ComponentModel.DataAnnotations;

namespace News.Contracts.V1.Requests
{
    public class UserSelfUpdateRequest
    {
        public string userId { get; set; }
        public string Password { get; set; }
    }
}