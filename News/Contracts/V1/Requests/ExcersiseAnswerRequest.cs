namespace News.Contracts.V1.Requests
{
    public class ExcersiseAnswerRequest
    {
        public string userId { get; set; }
        public string taskId { get; set; }
        public string answer { get; set; }
    }
}
