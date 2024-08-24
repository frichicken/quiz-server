public class QuestionAccount
{
    public int Id { get; set; }
    public bool IsStarred { get; set; }
    public int? AccountId { get; set; }
    public Account? Account { get; set; } = null!;
    public int? QuestionId { get; set; }
    public Question? Question { get; set; } = null!;
}