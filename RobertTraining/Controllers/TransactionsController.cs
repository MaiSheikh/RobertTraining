using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RobertTraining.Data;
using RobertTraining.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RobertTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ContextDb _context;
        
        public TransactionsController(ContextDb context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: api/<TransactionsController>
        [HttpGet]
        public async Task<IEnumerable<Models.Transaction>> Get()
        {
            return await _context.Transactions.ToListAsync();
        }

        // GET api/<TransactionsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transaction = await _context.Transactions.SingleOrDefaultAsync(i => i.Id == id);
            
            return transaction == null ? NotFound() : Ok(transaction);
        }

        // POST api/<TransactionsController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);

            var account = await _context.Accounts.SingleOrDefaultAsync(c => c.Id == transaction.AccountId);
            
            // TODO - Ifall account är null (med detta så kan det vara läge att läsa på om exception-middleware:
            // https://jasonwatmore.com/post/2022/01/17/net-6-global-error-handler-tutorial-with-example

            if (account == null) throw new NullReferenceException();
            
            account.Balance = account.Balance + transaction.Delta;
            
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction);
        }
        
        // DELETE api/<TransactionsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _context.Transactions.SingleOrDefaultAsync(i => i.Id == id);
            
            if (transaction == null) return NotFound();
            
            _context.Transactions.Remove(transaction);
            
            var account = await _context.Accounts.SingleOrDefaultAsync(c => c.Id == transaction.AccountId);

            if (account is null) throw new NullReferenceException("Med ett bra meddelande");
            
            account.Balance -= transaction.Delta;
            
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
