using Business_Logic_Layer;
using Business_Logic_Layer.Features.Account.Models;
using Business_Logic_Layer.Features.Account;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RobertTraining.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly AccountBLL _bll;
        
    public AccountsController(AccountBLL bll)
    {
        _bll = bll ?? throw new ArgumentNullException(nameof(bll));
    }
        
    [HttpGet]
    public async Task<IEnumerable<AccountModel>> Get()
    {
        return await _bll.GetAllAcountsFromBLL();
    }

    // GET api/<AccountsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AccountModel>> GetById(int id)
    {
        var result = await _bll.GetAccountByIdFromBLL(id);

        return Ok(result);
    }

    // POST api/<AccountsController>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] AccountModel accountModel)
    {
        var result = await _bll.CreateAccountFromBLL(accountModel);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    // PUT api/<AccountsController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] AccountModel accountModel)
    {         
        await _bll.UpdateAccountFromBLL(accountModel);

        return NoContent();
    }

    // DELETE api/<AccountsController>/5
    [HttpDelete("{id}")]
    public async Task <ActionResult> Delete(int id)
    {                     
        await _bll.DeleteAccountFromBLL(id);

        return NoContent();
    }
}