using AutoMapper;
using Business_Logic_Layer.Features.Account.Models;
using Data_Access_Layer.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic_Layer.Features.Account.Queries;

public class GetAllAccountsQuery : IRequest<IEnumerable<AccountModel>>
{
}

public class GetAllAcoountsQueryHandler : IRequestHandler<GetAllAccountsQuery, IEnumerable<AccountModel>>
{
    private readonly ContextDb _context;
    private readonly IMapper _mapper;

    public GetAllAcoountsQueryHandler(ContextDb context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<AccountModel>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        var accountsFromDB = await _context.Accounts.ToListAsync();
        var accountsModel = _mapper.Map<List<Data_Access_Layer.Entities.Account>, List<AccountModel>>(accountsFromDB);
        return accountsModel;

       // return await Task.FromResult(new List<AccountModel>());
    }
}