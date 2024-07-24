using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CollectionWithTotalQuizzesDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastModified { get; set; } = DateTime.Now;
    public List<int> QuizIds { get; set; } = [];
    public int TotalQuizzes { get; set; }
}

public class CollectionWithQuizzesDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastModified { get; set; } = DateTime.Now;
    public List<QuizWithTotalQuestionsDto> Quizzes { get; set; } = [];
}

[ApiController]
[Route("api/accounts/{accountId}/collections")]
public class CollectionController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CollectionController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<CollectionWithTotalQuizzesDto>>> Get([FromRoute] int accountId)
    {
        return await _context.Collections.Where(it => it.AccountId == accountId).Include(it => it.Quizzes).Select(it => new CollectionWithTotalQuizzesDto
        {
            Id = it.Id,
            Title = it.Title,
            Description = it.Description,
            CreatedAt = it.CreatedAt,
            LastModified = it.LastModified,
            TotalQuizzes = it.Quizzes.Count(),
            QuizIds = it.Quizzes.Select(it => it.Id).ToList()
        }).ToListAsync();
    }

    [HttpGet("{collectionId}")]
    public async Task<ActionResult<Collection>> GetById([FromRoute] int accountId, [FromRoute] int collectionId)
    {
        var stuff = await _context.Collections.Where(it => it.AccountId == accountId).FirstOrDefaultAsync(it => it.Id == collectionId);

        if (stuff is null) return NotFound();

        return stuff;
    }

    [HttpGet("{collectionId}/details")]
    public async Task<ActionResult<CollectionWithQuizzesDto>> GetDetailsById([FromRoute] int accountId, [FromRoute] int collectionId)
    {
        var stuff = await _context.Collections.Where(it => it.AccountId == accountId).Include(it => it.Quizzes).ThenInclude(it => it.Questions).FirstOrDefaultAsync(it => it.Id == collectionId);

        if (stuff is null) return NotFound();

        var stuffDto = new CollectionWithQuizzesDto
        {
            Id = stuff.Id,
            Title = stuff.Title,
            CreatedAt = stuff.CreatedAt,
            Description = stuff.Description,
            LastModified = stuff.LastModified,
            Quizzes = stuff.Quizzes.Select(it => new QuizWithTotalQuestionsDto
            {
                Id = it.Id,
                Title = it.Title,
                Description = it.Description,
                Status = it.Status,
                CreatedAt = it.CreatedAt,
                LastModified = it.LastModified,
                TotalQuestions = it.Questions.Count
            }).ToList()
        };

        return stuffDto;
    }

    [HttpPost]
    public async Task<ActionResult<Collection>> Create([FromRoute] int accountId, [FromBody] Collection collection)
    {
        var stuff = new Collection
        {
            Title = collection.Title,
            Description = collection.Description,
            AccountId = accountId
        };

        await _context.Collections.AddAsync(stuff);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { accountId, collectionId = stuff.Id }, stuff);
    }

    [HttpPut("{collectionId}")]
    public async Task<IActionResult> Update([FromRoute] int accountId, [FromRoute] int collectionId, [FromBody] Collection collection)
    {
        if (collectionId != collection.Id) return BadRequest();

        var stuff = await _context.Collections.Where(it => it.AccountId == accountId).FirstOrDefaultAsync(it => it.Id == collectionId);
        if (stuff is null) return NotFound();

        stuff.Title = collection.Title;
        stuff.Description = collection.Description;
        stuff.LastModified = DateTime.Now;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{collectionId}")]
    public async Task<IActionResult> Delete([FromRoute] int accountId, [FromRoute] int collectionId)
    {
        var stuff = await _context.Collections.Where(it => it.AccountId == accountId).Include(it => it.Quizzes).FirstOrDefaultAsync(it => it.Id == collectionId);

        if (stuff is null) return NotFound();

        _context.Collections.Remove(stuff);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{collectionId}/add-quiz/{quizId}")]
    public async Task<IActionResult> AddQuizToCollection([FromRoute] int accountId, [FromRoute] int collectionId, [FromRoute] int quizId)
    {
        var collection = await _context.Collections.Where(it => it.AccountId == accountId).FirstOrDefaultAsync(it => it.Id == collectionId);
        var quiz = await _context.Quizzes.Where(it => it.AccountId == accountId).FirstOrDefaultAsync(it => it.Id == quizId);

        if (quiz is null || collection is null) return NotFound();

        collection.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{collectionId}/delete-quiz/{quizId}")]
    public async Task<IActionResult> DeleteQuizFromCollection([FromRoute] int accountId, [FromRoute] int collectionId, [FromRoute] int quizId)
    {
        var collection = await _context.Collections.Where(it => it.AccountId == accountId).Include(it => it.Quizzes).FirstOrDefaultAsync(it => it.Id == collectionId);

        if (collection is null) return NotFound();
        var quiz = collection.Quizzes.Find(it => it.Id == quizId);

        if (quiz is null) return NotFound();
        collection.Quizzes.Remove(quiz);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}