using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Business_Logic_Layer.Features;
using Business_Logic_Layer.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RobertTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private Business_Logic_Layer.Features.TransactionBLL _bll;


        public TransactionsController(TransactionBLL bll)
        {
            _bll = bll ?? throw new ArgumentNullException(nameof(bll));
        }

        // GET: api/<TransactionsController>
        [HttpGet]
        public async Task<IEnumerable<TransactionModel>> Get()
        {
            return await _bll.GetAllTransactionsFromBLL();
        }

        // GET api/<TransactionsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionModel>> GetById(int id)
        {
            var result = await _bll.GetTransactionByIdFromBLL(id);

            return Ok(result);
        }

        // POST api/<TransactionsController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionModel transactionModel)
        {            
            var result = await _bll.CreateTransactionFromBLL(transactionModel);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // DELETE api/<TransactionsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            await _bll.DeleteTransactionFromBLL(id);

            return NoContent();
        }
    }
}
