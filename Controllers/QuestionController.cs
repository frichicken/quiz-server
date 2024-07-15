using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/quizzes/{quizId}/questions")]
public class QuestionController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public QuestionController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("/api/questions")]
    public async Task<ActionResult<List<Question>>> Get()
    {
        return await _context.Questions.ToListAsync();
    }

    [HttpGet("/api/questions/{id}")]
    public async Task<ActionResult<Question>> GetById([FromRoute] int id)
    {
        var question = await _context.Questions.FindAsync(id);

        if (question is null) return NotFound();
        return question;
    }

    [HttpGet]
    public async Task<ActionResult<List<Question>>> Get([FromRoute] int quizId)
    {
        return await _context.Questions.Where(it => it.QuizId == quizId).ToListAsync();
    }

    [HttpGet("{questionId}")]
    public async Task<ActionResult<Question>> GetById([FromRoute] int quizId, [FromRoute] int questionId)
    {
        var question = await _context.Questions.Where(question => question.QuizId == quizId).FirstOrDefaultAsync(question => question.Id == questionId);

        if (question is null) return NotFound();

        return question;
    }

    [HttpPost]
    public async Task<ActionResult<Question>> Create([FromRoute] int quizId, [FromBody] Question question)
    {
        var stuff = new Question
        {
            Text = question.Text,
            QuizId = quizId
        };

        _context.Questions.Add(stuff);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = stuff.Id }, stuff);
    }

    [HttpDelete("{questionId}")]
    public async Task<IActionResult> Delete([FromRoute] int quizId, [FromRoute] int questionId)
    {
        var question = await _context.Questions.Include(it => it.Answers).Where(question => question.QuizId == quizId).FirstOrDefaultAsync(question => question.Id == questionId);

        if (question is null) return NotFound();

        _context.Questions.Remove(question);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{questionId}")]
    public async Task<IActionResult> Update([FromRoute] int quizId, [FromRoute] int questionId, [FromBody] Question question)
    {
        if (questionId != question.Id) return BadRequest();

        var stuff = await _context.Questions.Where(question => question.QuizId == quizId).FirstOrDefaultAsync(question => question.Id == questionId);
        if (stuff is null) return NotFound();

        stuff.Text = question.Text;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}