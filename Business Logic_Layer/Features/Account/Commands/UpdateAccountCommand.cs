using AutoMapper;
using Business_Logic_Layer.Features.Account.Models;
using Data_Access_Layer.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Features.Account.Commands
{
 
        public class UpdateAccountCommand : IRequest<Data_Access_Layer.Entities.Account>
        {
            public AccountModel _accountModel { get; init; }
            public UpdateAccountCommand(AccountModel accountModel)
            {
                _accountModel = accountModel ?? throw new ArgumentNullException(nameof(_accountModel));
            }
        }

        public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, Data_Access_Layer.Entities.Account>
        {
            private readonly ContextDb _context;
            private readonly IMapper _mapper;

            public UpdateAccountCommandHandler(ContextDb context, IMapper mapper)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

        public async Task<Data_Access_Layer.Entities.Account> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            if (request._accountModel == null) throw new ArgumentNullException(nameof(request._accountModel));
            var account = _mapper.Map<AccountModel, Data_Access_Layer.Entities.Account>(request._accountModel);
            if (account == null) throw new ArgumentNullException(nameof(account));

            //  _context.Entry(account).State = EntityState.Modified;
            _context.Accounts.Update(account);


            await _context.SaveChangesAsync();
            return account;
        }
       
    }
}
