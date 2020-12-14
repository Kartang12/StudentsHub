namespace News.Contracts.V1.Requests
{
    public class UserDataUpdateRequest
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}