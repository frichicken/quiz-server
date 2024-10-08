using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    static readonly string connectionString = "Server=localhost; User ID=root; Password=root; Database=quiz";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), options =>
        {
            options.EnableStringComparisonTranslations();
        });
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Collection> Collections { get; set; }

}