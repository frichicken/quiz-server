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

    // Validate email and password and check if it exists or not then register a new account
    [HttpPost]
    [Route("/sign-up")]
    public async Task<ActionResult<Account>> SignUp([FromBody] Account account)
    {
        var email = account.Email.Trim();
        var password = account.Password.Trim();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) return BadRequest();

        if (Utilities.IsValidEmail(email) == false) return BadRequest();

        if (await _context.Accounts.AnyAsync(account => account.Email.Trim().Equals(email)))
            return BadRequest();
        else
        {
            var newAccount = new Account
            {
                Email = account.Email,
                Description = account.Description,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Password = Utilities.Hash(account.Password),
                Username = account.Username
            };

            await _context.Accounts.AddAsync(newAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newAccount.Id }, newAccount);
        }
    }

    // Validate email and password and check if it exists or not then log in
    [HttpPost]
    [Route("/log-in")]
    public async Task<ActionResult<AccountResponseDto>> LogIn([FromBody] Account account)
    {
        var email = account.Email.Trim();
        var password = account.Password.Trim();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) return BadRequest();

        if (Utilities.IsValidEmail(email) == false) return BadRequest();

        var stuff = await _context.Accounts.FirstOrDefaultAsync(it => it.Email.Trim().Equals(email) && it.Password.Trim().Equals(Utilities.Hash(password)));

        if (stuff is null)
            return NotFound();

        stuff.SessionId = Utilities.GetRandomString();
        stuff.ExpiresIn = DateTime.Now.AddMinutes(30);

        await _context.SaveChangesAsync();

        return new AccountResponseDto
        {
            Id = stuff.Id,
            Email = stuff.Email,
            Description = stuff.Description,
            FirstName = stuff.FirstName,
            LastName = stuff.LastName,
            ExpiresIn = stuff.ExpiresIn,
            SessionId = stuff.SessionId
        };
    }
}