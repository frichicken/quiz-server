public class QuizAccount
{
    public int Id { get; set; }
    public DateTime LastAccess { get; set; } = DateTime.Now;
    public bool IsSaved { get; set; }
    public int? AccountId { get; set; }
    public Account? Account { get; set; } = null!;
    public int? QuizId { get; set; }
    public Quiz? Quiz { get; set; } = null!;
}