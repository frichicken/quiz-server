using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CollectionWithTotalQuizzesDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastModified { get; set;} = DateTime.Now;
    public int TotalQuizzes { get; set; }
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
        return await _context.Collections.Where(it => it.AccountId == accountId).Include(it => it.Quizzes).Select(it => new CollectionWithTotalQuizzesDto {
            Id = it.Id,
            Title = it.Title,
            Description = it.Description,
            CreatedAt = it.CreatedAt,
            LastModified = it.LastModified,
            TotalQuizzes = it.Quizzes.Count()
        }).ToListAsync();
    }

    [HttpGet("{collectionId}")]
    public async Task<ActionResult<Collection>> GetById([FromRoute] int accountId, [FromRoute] int collectionId)
    {
        var stuff =await _context.Collections.Where(it => it.AccountId == accountId).FirstOrDefaultAsync(it => it.Id == collectionId);

        if (stuff is null) return NotFound();
        
        return stuff;
    }

    [HttpPost]
    public async Task<ActionResult<Collection>> Create([FromRoute] int accountId, [FromBody] Collection collection)
    {
        var newCollection = new Collection {
            Title = collection.Title,
            Description = collection.Description,
            AccountId = accountId
        };

        await _context.Collections.AddAsync(newCollection);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = newCollection.Id }, newCollection);
    }   
}