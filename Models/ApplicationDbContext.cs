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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Quiz>()
            .HasMany(entity => entity.Accounts)
            .WithMany(entity => entity.Quizzes)
            .UsingEntity<QuizAccount>();

        modelBuilder.Entity<Question>()
            .HasMany(entity => entity.Accounts)
            .WithMany(entity => entity.Questions)
            .UsingEntity<QuestionAccount>();
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<QuizAccount> QuizAccount { get; set; }
    public DbSet<QuestionAccount> QuestionAccount { get; set; }

}