namespace News.Contracts.V1.Requests
{
    public class ExerciseUpdateRequest
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
