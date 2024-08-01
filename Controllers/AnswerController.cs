using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/questions/{questionId}/answers")]
public class AnswerController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AnswerController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("/api/answers")]
    public async Task<ActionResult<List<Answer>>> Get()
    {
        return await _context.Answers.ToListAsync();
    }

    [HttpGet("/api/answers/{id}")]
    public async Task<ActionResult<Answer>> GetById([FromRoute] int id)
    {
        var answer = await _context.Answers.FindAsync(id);

        if (answer is null) return NotFound();

        return answer;
    }

    [HttpGet]
    public async Task<ActionResult<List<Answer>>> Get([FromRoute] int questionId)
    {
        return await _context.Answers.Where(it => it.QuestionId == questionId).ToListAsync();
    }

    [HttpGet("{answerId}")]
    public async Task<ActionResult<Answer>> GetById([FromRoute] int questionId, [FromRoute] int answerId)
    {
        var answer = await _context.Answers.Where(answer => answer.QuestionId == questionId).FirstOrDefaultAsync(answer => answer.Id == answerId);

        if (answer is null) return NotFound();

        return answer;
    }

    [HttpPost]
    public async Task<ActionResult<Answer>> Create([FromRoute] int questionId, [FromBody] Answer answer)
    {
        var stuff = new Answer
        {
            Text = answer.Text,
            QuestionId = questionId,
            IsCorrect = answer.IsCorrect,
        };

        await _context.Answers.AddAsync(stuff);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { questionId, answerId = stuff.Id }, stuff);
    }

    [HttpDelete("{answerId}")]
    public async Task<IActionResult> Delete([FromRoute] int questionId, [FromRoute] int answerId)
    {
        var answer = await _context.Answers.Where(answer => answer.QuestionId == questionId).FirstOrDefaultAsync(answer => answer.Id == answerId);

        if (answer is null) return NotFound();

        _context.Answers.Remove(answer);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{answerId}")]
    public async Task<IActionResult> Update([FromRoute] int questionId, [FromRoute] int answerId, [FromBody] Answer answer)
    {
        if (answerId != answer.Id) return BadRequest();

        var stuff = await _context.Answers.Where(answer => answer.QuestionId == questionId).FirstOrDefaultAsync(answer => answer.Id == answerId);
        if (stuff is null) return NotFound();

        stuff.Text = answer.Text;
        stuff.IsCorrect = answer.IsCorrect;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("update-multiple")]
    public async Task<IActionResult> UpdateMultiple([FromRoute] int questionId, [FromBody] List<Answer> answers)
    {
        var stuffs = await _context.Answers.Where(answer => answer.QuestionId == questionId).ToListAsync();

        try
        {
            answers.ForEach(it =>
            {
                var updateAnswer = stuffs.Find(stuff => stuff.Id == it.Id);

                if (updateAnswer is null) throw new Exception("");
                
                updateAnswer.IsCorrect = it.IsCorrect;
                updateAnswer.Text = it.Text;
            });
        }
        catch
        {
            return NotFound();
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }
}