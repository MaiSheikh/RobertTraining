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
        public TransactionsController(ContextDb con)
        {
            _context = con;
        }

        // GET: api/<TransactionsController>
        [HttpGet]
        public async Task<IEnumerable<Models.Transaction>> Get()
        {
            return await _context.Transactions.ToListAsync();
        }

        // GET api/<TransactionsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var transaction = await _context.Transactions.Where(b => b.Id == id).FirstOrDefaultAsync();
            

            return transaction == null ? NotFound() : Ok(transaction);
        }

        // POST api/<TransactionsController>
        [HttpPost]
        public  async Task<IActionResult> Create([FromBody] Transaction transaction)
        {

            await _context.Transactions.AddAsync(transaction);
          Account acc=  _context.Accounts.Single(c => c.Id == transaction.AccountId);
            if (acc != null)
            {
                acc.Balance = acc.Balance + transaction.Delta;
            }
            else
                return BadRequest();

            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetByID), new { id = transaction.Id }, transaction);
            // Account account = _context.Accounts.Single(c => c.Id == transaction.AccountId);
            //Transaction t = new Transaction
            //{

            //    Delta = transaction.Delta,
            //    AccountId = transaction.AccountId,
            //    TimeStamp = DateTime.Now,
            //    Account = account

            //};
            //await _context.Transactions.AddAsync(t);

            //await _context.SaveChangesAsync();


           // return CreatedAtAction(nameof(GetByID), new { id = transaction.Id }, transaction);
        }

        // PUT api/<TransactionsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Transaction transaction)
        {
            if (id != transaction.Id) return BadRequest();
            
           var acc = _context.Accounts.AsNoTracking().Include(c => c.Transactions).FirstOrDefault(c=>c.Id==id);
            var oldDelta = acc.Transactions.FirstOrDefault(c => c.Id == id).Delta;
            //_context.ChangeTracker.Clear();

            _context.Entry(transaction).State = EntityState.Modified;
            


            if (acc != null)
            {
                if (oldDelta < transaction.Delta)
                {
                    var difference = transaction.Delta - oldDelta;
                    acc.Balance = acc.Balance + difference;
                }
                else if (oldDelta > transaction.Delta)
                {
                    var difference = oldDelta - transaction.Delta ;


                    acc.Balance = acc.Balance - difference;
                }
            }
            else
                return BadRequest();
            _context.Entry(acc).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<TransactionsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var transactionToRemove = await _context.Transactions.FindAsync(id);
            if (transactionToRemove == null) return NotFound();
            _context.Transactions.Remove(transactionToRemove);
            Account acc = _context.Accounts.Single(c => c.Id == transactionToRemove.AccountId);
            if (acc != null)
            {
                acc.Balance = acc.Balance - transactionToRemove.Delta;
            }
            else
                return BadRequest();
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
