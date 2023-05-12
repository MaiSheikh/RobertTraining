using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RobertTraining.Data;
using RobertTraining.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RobertTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ContextDb _context;
        public AccountsController(ContextDb con)
        {
            _context = con;
        }
        [HttpGet]
        public async Task<IEnumerable<Account>> Get()
        {
            return await _context.Accounts.ToListAsync();
        }

        // GET api/<AccountsController>/5
        [HttpGet("{id}")]
        public async Task< IActionResult >GetByID(int id)
        {
            var account = await _context.Accounts.Where(x => x.Id == id).FirstOrDefaultAsync();

     

            return account == null ? NotFound() : Ok(account) ;
        }

        // POST api/<AccountsController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Account account)
        {
            
            await _context.Accounts.AddAsync(account);
           
            await _context.SaveChangesAsync();

           
            return CreatedAtAction(nameof(GetByID), new { id = account.Id }, account);
        }

        // PUT api/<AccountsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Account account)
        {
            if (id != account.Id) return BadRequest();
            _context.Entry(account).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();

        }

        // DELETE api/<AccountsController>/5
        [HttpDelete("{id}")]
        public async Task< IActionResult> Delete(int id)
        {
            var accountToDelete = await _context.Accounts.FindAsync(id);
            if (accountToDelete == null) return NotFound();
            _context.Accounts.Remove(accountToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
