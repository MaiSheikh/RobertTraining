using AutoMapper;
using Business_Logic_Layer.Features.Account.Models;
using Data_Access_Layer.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic_Layer.Features.Account.Queries;

public class GetByIdQuery : IRequest<AccountModel>
{
    public int Id { get; init; }
    
    public GetByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, AccountModel>
{
    private readonly ContextDb _context;
    private readonly IMapper _mapper;

    public GetByIdQueryHandler(ContextDb context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    }
    
    public async Task<AccountModel> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        //var account = await _context.Accounts.SingleOrDefaultAsync(i => i.Id == request.Id, cancellationToken);
        //if (account == null) throw new ArgumentNullException(nameof(account));
        //var accountModel = _mapper.Map<Data_Access_Layer.Entities.Account, AccountModel>(account);

        var account = new AccountModel
        {
            Id = request.Id
        };
        
        return account;
    }
}