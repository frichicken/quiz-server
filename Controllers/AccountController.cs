using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/accounts")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetById([FromRoute] int id)
    {
        var account = await _context.Accounts.FindAsync(id);

        if (account is null) return NotFound();

        return account;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var account = await _context.Accounts.FindAsync(id);

        if (account is null) return NotFound();

        _context.Accounts.Remove(account);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Account account)
    {
        if (id != account.Id) return BadRequest();

        var stuff = await _context.Accounts.FindAsync(id);

        if (stuff is null) return NotFound();

        stuff.Email = account.Email;
        stuff.Description = account.Description;
        stuff.FirstName = account.FirstName;
        stuff.LastName = account.LastName;
        stuff.Password = account.Password;
        stuff.Username = account.Username;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost]
    [Route("/sign-up")]
    public async Task<ActionResult<Account>> SignUp([FromBody] Account account)
    {
        if (await _context.Accounts.AnyAsync(stuff => stuff.Email.Equals(account.Email)))
        {
            return BadRequest();
        }
        else
        {
            var stuff = new Account
            {
                Email = account.Email,
                Description = account.Description,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Password = account.Password, // it's not hashed
                Username = account.Username,
            };

            await _context.Accounts.AddAsync(stuff);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = stuff.Id }, stuff);
        }
    }

    [HttpPost]
    [Route("/log-in")]
    public async Task<ActionResult<Account>> LogIn([FromBody] Account account)
    {
        var stuff = await _context.Accounts.FirstOrDefaultAsync(it => it.Email.Equals(account.Email));

        if (stuff is null)
            return NotFound();

        if (stuff.Email.Equals(account.Email) && stuff.Password.Equals(account.Password))
            return stuff;

        return BadRequest();
    }
}