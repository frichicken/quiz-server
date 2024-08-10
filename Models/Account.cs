public class Account
{
    public int Id { get; set; }
    public string Email { get; set; } = "";
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string Description { get; set; } = "";
    public string LastName { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string SessionId { get; set; } = "";
    public DateTime ExpiresIn { get; set; }
    public List<Quiz> Quizzes { get; set; } = [];
    public List<Collection> Collections { get; set; } = [];
}