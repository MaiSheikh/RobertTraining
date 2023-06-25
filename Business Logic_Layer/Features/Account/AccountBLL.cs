using AutoMapper;
using Business_Logic_Layer.Features.Account.Models;
using Data_Access_Layer.Data;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic_Layer.Features.Account;

public class AccountBLL
{
    private readonly ContextDb _context;
    private readonly IMapper _accountMapper;

    public AccountBLL(ContextDb context, IMapper? mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _accountMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

   

    public async Task<IEnumerable<AccountModel>> GetAllAcountsFromBLL()
    {
        var accountsFromDB = await _context.Accounts.ToListAsync();
        var accountsModel = _accountMapper.Map<List<Data_Access_Layer.Entities.Account>, List<AccountModel>>(accountsFromDB);
        return accountsModel;
        // return await Task.FromResult( accountsModel);
    }



    public async Task<AccountModel> GetAccountByIdFromBLL(int id)
    {
        var account = await _context.Accounts.SingleOrDefaultAsync(i => i.Id == id);
        if (account == null) throw new ArgumentNullException(nameof(account));
        var accountModel = _accountMapper.Map<Data_Access_Layer.Entities.Account, AccountModel>(account);

        return accountModel;
    }

    public async Task<Data_Access_Layer.Entities.Account> CreateAccountFromBLL(AccountModel accountModel)
    {
        var account = _accountMapper.Map<AccountModel, Data_Access_Layer.Entities.Account>(accountModel);

        await _context.Accounts.AddAsync(account);
        _context.SaveChanges();

        return account;
    }

    public async Task UpdateAccountFromBLL(AccountModel accountModel)
    {
        if (accountModel == null) throw new ArgumentNullException(nameof(accountModel));
        var account = _accountMapper.Map<AccountModel, Data_Access_Layer.Entities.Account>(accountModel);
        if (account == null) throw new ArgumentNullException(nameof(account));

        _context.Entry(account).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAccountFromBLL(int id)
    {
        var account = await _context.Accounts.SingleOrDefaultAsync(i => i.Id == id);
        if (account == null) throw new ArgumentNullException(nameof(account));

        _context.Accounts.Remove(account);
        _context.SaveChanges();
    }
}