public class QuestionResponseDto
{
    public int Id { get; set; }
    public string Text { get; set; } = "";
    public bool IsStarred { get; set; }
    public int Type { get; set; } = (int)QuestionTypes.Single;
    public List<Answer> Answers { get; set; } = [];

}