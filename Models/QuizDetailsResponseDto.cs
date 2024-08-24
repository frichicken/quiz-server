public class QuizDetailsResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public int Status { get; set; } = (int)QuizStatuses.Draft;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastModified { get; set; } = DateTime.Now;
    public List<QuestionResponseDto> Questions { get; set; } = [];
    public bool IsSaved { get; set; }
    public AccountResponseGetQuizList? CreatedBy { get; set; } = null!;
}