public class Question
{
    public int Id { get; set; }
    public string Text { get; set; } = "";
    public bool IsStarred { get; set; }
    public int Type { get; set; } = (int) QuestionTypes.Single;
    public int? QuizId { get; set; }
    public Quiz? Quiz { get; set; }
    public List<Answer> Answers { get; set; } = [];
}