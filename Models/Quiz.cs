public class Quiz
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public int Status { get; set; } = (int)QuizStatuses.Draft;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastModified { get; set; } = DateTime.Now;
    public bool IsSaved { get; set; }
    public List<Question> Questions { get; set; } = [];
    public int? AccountId { get; set; }
    public Account? Account { get; set; } = null!;
    public List<Collection> Collections { get; set; } = [];
}