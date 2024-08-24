using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/accounts/{accountId}/quizzes")]
public class QuizController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public QuizController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("/api/quizzes/{id}")]
    public async Task<ActionResult<Quiz>> GetById([FromRoute] int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);

        if (quiz is null) return NotFound();

        return quiz;
    }


    [HttpPost]
    public async Task<ActionResult<Quiz>> Create([FromRoute] int accountId, [FromBody] Quiz quiz)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(account => account.Id == accountId);
        if (account == null) return BadRequest();

        var newQuiz = new Quiz
        {
            Title = quiz.Title,
            Description = quiz.Description,
            Status = (int)QuizStatuses.Draft,
            CreatedBy = accountId
        };

        await _context.Quizzes.AddAsync(newQuiz);
        account.Quizzes.Add(newQuiz);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = newQuiz.Id }, newQuiz);
    }

    [HttpGet]
    public async Task<ActionResult<List<QuizResponseDto>>> GetQuizList([FromRoute] int accountId, [FromQuery] string? keyword, [FromQuery] int? filter = (int)FilterTypes.Recent)
    {
        var quizAccounts = _context.QuizAccount.Include(qa => qa.Quiz).Where(qa => qa.AccountId == accountId);

        if (filter == (int)FilterTypes.Draft) quizAccounts = quizAccounts.Where(qa => qa.Quiz.Status == (int)FilterTypes.Draft && qa.Quiz.CreatedBy == accountId);

        if (filter == (int)FilterTypes.Published) quizAccounts = quizAccounts.Where(qa => qa.Quiz.Status == (int)FilterTypes.Published);

        if (filter == (int)FilterTypes.Saved) quizAccounts = quizAccounts.Where(qa => qa.Quiz.Status == (int)FilterTypes.Published && qa.IsSaved);

        if (string.IsNullOrEmpty(keyword) == false && string.IsNullOrWhiteSpace(keyword) == false)
            quizAccounts = quizAccounts.Where(qa => qa.Quiz.Title.Trim().Contains(keyword.Trim(), StringComparison.CurrentCultureIgnoreCase) || keyword.Trim().Contains(qa.Quiz.Title.Trim(), StringComparison.CurrentCultureIgnoreCase));


        var response = await quizAccounts.Select(qa => new QuizResponseDto
        {
            Id = qa.Quiz.Id,
            Title = qa.Quiz.Title,
            Description = qa.Quiz.Description,
            Status = qa.Quiz.Status,
            CreatedAt = qa.Quiz.CreatedAt,
            CreatedBy = new AccountResponseGetQuizList
            {
                Id = _context.Accounts.FirstOrDefault(account => account.Id == qa.Quiz.CreatedBy).Id,
                Email = _context.Accounts.FirstOrDefault(account => account.Id == qa.Quiz.CreatedBy).Email,
            },
            LastModified = qa.Quiz.LastModified,
            TotalQuestions = qa.Quiz.Questions.Count,
            IsSaved = qa.IsSaved
        }).ToListAsync();

        return response;
    }

    [HttpGet("/api/accounts/{createdBy}/quizzes/{quizId}/qa/{accountId}")]
    public async Task<ActionResult<QuizDetailsResponseDto>> ForDetails([FromRoute] int createdBy, [FromRoute] int quizId, [FromRoute] int accountId)
    {
        var quiz = await _context.Quizzes
            .Include(it => it.Questions)
            .ThenInclude(it => it.Answers)
            .Include(it => it.Questions)
            .FirstOrDefaultAsync(quiz => quiz.Id == quizId && quiz.CreatedBy == createdBy);

        if (quiz is null) return NotFound();

        var response = new QuizDetailsResponseDto
        {
            Id = quiz.Id,
            CreatedAt = quiz.CreatedAt,
            LastModified = quiz.LastModified,
            Description = quiz.Description,
            IsSaved = _context.QuizAccount.FirstOrDefault(it => it.AccountId == accountId && quiz.Id == it.QuizId).IsSaved,
            Status = quiz.Status,
            CreatedBy = new AccountResponseGetQuizList
            {
                Id = _context.Accounts.Find(createdBy).Id,
                Email = _context.Accounts.Find(createdBy).Email,
            },
            Questions = quiz.Questions.Select(it => new QuestionResponseDto
            {
                Id = it.Id,
                Answers = it.Answers,
                Text = it.Text,
                IsStarred = _context.QuestionAccount.FirstOrDefault(qa => qa.AccountId == accountId && qa.QuestionId == it.Id).IsStarred,
                Type = it.Type
            }).ToList()
        };

        return response;
    }

    [HttpGet("{quizId}")]
    public async Task<ActionResult<Quiz>> ForEditing([FromRoute] int accountId, [FromRoute] int quizId)
    {
        var quiz = await _context.Quizzes
            .Include(it => it.Questions)
            .ThenInclude(it => it.Answers)
            .Include(it => it.Questions)
            .FirstOrDefaultAsync(quiz => quiz.Id == quizId && quiz.CreatedBy == accountId);

        if (quiz is null) return NotFound();

        return quiz;
    }

    [HttpPut("{quizId}/handle-save")]
    public async Task<IActionResult> HandleSave([FromRoute] int accountId, [FromRoute] int quizId, [FromBody] bool IsSaved)
    {
        var stuff = await _context.QuizAccount.FirstOrDefaultAsync(qa => qa.QuizId == quizId && qa.AccountId == accountId);

        if (stuff is null) return NotFound();

        stuff.IsSaved = IsSaved;

        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{quizId}")]
    public async Task<IActionResult> Delete([FromRoute] int accountId, [FromRoute] int quizId)
    {
        var quiz = await _context.Quizzes.Include(it => it.Collections).Include(it => it.Accounts).Include(it => it.Questions).ThenInclude(it => it.Answers).FirstOrDefaultAsync(quiz => quiz.Id == quizId);
        if (quiz is null) return NotFound();

        if (quiz.CreatedBy == accountId)
        {
            // quiz.Collections.Clear();
            // quiz.Questions.ForEach(it => it.Answers.Clear());
            // quiz.Questions.Clear();
            // quiz.Accounts.Clear();
            // _context.Quizzes.Remove(quiz);
            _context.Remove(quiz);
        }
        else
        {
            var qa = await _context.Accounts.FirstOrDefaultAsync(it => it.Id == accountId);

            if (qa is null) return NotFound();

            quiz.Accounts.Remove(qa);
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{quizId}")]
    public async Task<IActionResult> Update([FromRoute] int accountId, [FromRoute] int quizId, [FromBody] Quiz quiz)
    {
        if (quizId != quiz.Id) return BadRequest();

        var stuff = await _context.Quizzes.FirstOrDefaultAsync(quiz => quiz.Id == quizId && quiz.CreatedBy == accountId);

        if (stuff is null) return NotFound();

        stuff.Title = quiz.Title;
        stuff.Description = quiz.Description;
        stuff.LastModified = DateTime.Now;
        stuff.Status = quiz.Status;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}