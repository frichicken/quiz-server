public class Collection
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastModified { get; set;} = DateTime.Now;
    public List<Quiz> Quizzes { get; set; } = [];
    public int? AccountId { get; set; }
    public Account? Account { get; set; }
}