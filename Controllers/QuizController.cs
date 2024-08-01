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

    [HttpGet("/api/quizzes")]
    public async Task<ActionResult<List<Quiz>>> Get()
    {
        return await _context.Quizzes.ToListAsync();
    }

    [HttpGet("/api/quizzes/{id}")]
    public async Task<ActionResult<Quiz>> GetById([FromRoute] int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);

        if (quiz is null) return NotFound();

        return quiz;
    }

    [HttpGet]
    public async Task<ActionResult<List<QuizWithTotalQuestionsDto>>> Get([FromRoute] int accountId)
    {
        return await _context.Quizzes.Where(quiz => quiz.AccountId == accountId).Include(it => it.Questions).Select(it => new QuizWithTotalQuestionsDto
        {
            Id = it.Id,
            Title = it.Title,
            Description = it.Description,
            Status = it.Status,
            CreatedAt = it.CreatedAt,
            IsSaved = it.IsSaved,
            LastModified = it.LastModified,
            TotalQuestions = it.Questions.Count()
        }).ToListAsync();
    }

    [HttpGet("{quizId}")]
    public async Task<ActionResult<Quiz>> GetById([FromRoute] int accountId, [FromRoute] int quizId)
    {
        var quiz = await _context.Quizzes.Where(quiz => quiz.AccountId == accountId).FirstOrDefaultAsync(quiz => quiz.Id == quizId);

        if (quiz is null) return NotFound();

        return quiz;
    }

    [HttpGet("{quizId}/details")]
    public async Task<ActionResult<Quiz>> GetDetailsById([FromRoute] int accountId, [FromRoute] int quizId)
    {
        var quiz = await _context.Quizzes.Include(quiz => quiz.Questions).ThenInclude(question => question.Answers).Where(quiz => quiz.AccountId == accountId).FirstOrDefaultAsync(quiz => quiz.Id == quizId);

        if (quiz is null) return NotFound();

        return quiz;
    }

    [HttpPost]
    public async Task<ActionResult<Quiz>> Create([FromRoute] int accountId, [FromBody] Quiz quiz)
    {
        var stuff = new Quiz
        {
            Title = quiz.Title,
            Description = quiz.Description,
            Status = (int)QuizStatuses.Draft,
            AccountId = accountId
        };

        await _context.Quizzes.AddAsync(stuff);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { accountId, quizId = stuff.Id }, stuff);
    }

    [HttpDelete("{quizId}")]
    public async Task<IActionResult> Delete([FromRoute] int accountId, [FromRoute] int quizId)
    {
        var quiz = await _context.Quizzes.Include(it => it.Questions).ThenInclude(it => it.Answers).Where(quiz => quiz.AccountId == accountId).FirstOrDefaultAsync(quiz => quiz.Id == quizId);

        if (quiz is null) return NotFound();

        _context.Quizzes.Remove(quiz);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{quizId}")]
    public async Task<IActionResult> Update([FromRoute] int accountId, [FromRoute] int quizId, [FromBody] Quiz quiz)
    {
        if (quizId != quiz.Id) return BadRequest();

        var stuff = await _context.Quizzes.Where(quiz => quiz.AccountId == accountId).FirstOrDefaultAsync(quiz => quiz.Id == quizId);
        if (stuff is null) return NotFound();

        stuff.Title = quiz.Title;
        stuff.Description = quiz.Description;
        stuff.LastModified = DateTime.Now;
        stuff.Status = quiz.Status;
        stuff.IsSaved = quiz.IsSaved;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}