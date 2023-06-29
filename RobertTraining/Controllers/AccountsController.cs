using Business_Logic_Layer;
using Business_Logic_Layer.Features.Account.Models;
using Business_Logic_Layer.Features.Account;
using Business_Logic_Layer.Features.Account.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Business_Logic_Layer.Features.Account.Commands;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RobertTraining.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly AccountBLL _bll;
    private IMediator _mediator;
        
    public AccountsController(AccountBLL bll, IMediator mediator)
    {
        _bll = bll ?? throw new ArgumentNullException(nameof(bll));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
        
    [HttpGet]
    public async Task<IEnumerable<AccountModel>> Get()
    {
        return await _mediator.Send(new GetAllAccountsQuery());
    }

    // GET api/<AccountsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AccountModel>> GetById(int id)
    {
        var result = await _mediator.Send(new GetByIdQuery(id));

        return Ok(result);
    }

    // POST api/<AccountsController>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] AccountModel accountModel)
    {
       // var result = await _bll.CreateAccountFromBLL(accountModel);
       var result= await _mediator.Send(new CreateAccountCommand(accountModel));

       // return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
       return Ok(result);   
    }

    // PUT api/<AccountsController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] AccountModel accountModel)
    {
        var result = await _mediator.Send(new UpdateAccountCommand(accountModel));

        // return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        return Ok(result);
        //await _bll.UpdateAccountFromBLL(accountModel);

        //return NoContent();
    }

    // DELETE api/<AccountsController>/5
    [HttpDelete("{id}")]
    public async Task <ActionResult> Delete(int id)
    {
        //await _bll.DeleteAccountFromBLL(id);
         await _mediator.Send(new DeleteAccountCommand(id));

        return NoContent();
    }
}