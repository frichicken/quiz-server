public class Quiz
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public int Status { get; set; } = (int)QuizStatuses.Draft;
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastModified { get; set; } = DateTime.Now;
    public List<Question> Questions { get; set; } = [];
    public List<Account> Accounts { get; set; } = [];
    public List<Collection> Collections { get; set; } = [];
    public List<QuizAccount> QuizAccounts { get; set; } = [];
}