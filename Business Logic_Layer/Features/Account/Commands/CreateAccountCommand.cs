using AutoMapper;
using Business_Logic_Layer.Features.Account.Models;
using Business_Logic_Layer.Features.Account.Queries;
using Data_Access_Layer.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Features.Account.Commands
{
    public class CreateAccountCommand : IRequest<Data_Access_Layer.Entities.Account>
    {
        public AccountModel _accountModel { get; init; }
        public CreateAccountCommand(AccountModel accountModel)
        {
            _accountModel = accountModel ?? throw new ArgumentNullException(nameof(_accountModel));
        }
    }

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Data_Access_Layer.Entities.Account>
       {
         private readonly ContextDb _context;
        private readonly IMapper _mapper;

    public CreateAccountCommandHandler(ContextDb context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Data_Access_Layer.Entities.Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = _mapper.Map<AccountModel, Data_Access_Layer.Entities.Account>(request._accountModel);

        await _context.Accounts.AddAsync(account);
        _context.SaveChanges();
            return account;
       
    }
}
}
    

