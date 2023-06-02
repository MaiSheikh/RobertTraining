using Microsoft.EntityFrameworkCore;
using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Business_Logic_Layer.Models;
using AutoMapper;
using Data_Access_Layer;
using System.Diagnostics.CodeAnalysis;

namespace Business_Logic_Layer
{
    public class AccountBLL
    {
        private ContextDb _context;
        private readonly IMapper _accountMapper;

        public AccountBLL(ContextDb context, IMapper mapper)
        {                      
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _accountMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public  async Task<IEnumerable<AccountModel>> GetAllAcountsFromBLL()
        {
            var accountsFromDB = await _context.Accounts.ToListAsync();
            var accountsModel = _accountMapper.Map<List<Account>, List<AccountModel>>(accountsFromDB);
            return accountsModel;
            // return await Task.FromResult( accountsModel);
        }

        public async Task<AccountModel> GetAccountByIdFromBLL(int id)
        {          
            var account = await _context.Accounts.SingleOrDefaultAsync(i => i.Id == id);
            if (account == null ) { throw new ArgumentNullException(nameof(account)); }
            var accountModel = _accountMapper.Map<Account, AccountModel>(account);

            return accountModel;
        }

        public async Task<Account> CreateAccountFromBLL(AccountModel accountModel)
        {
            var account =  _accountMapper.Map<AccountModel, Account>(accountModel);

           await _context.Accounts.AddAsync(account);
            _context.SaveChanges();

            return account;           
        }

        public async Task UpdateAccountFromBLL(AccountModel accountModel)
        {
            if (accountModel == null) { throw new ArgumentNullException(nameof(accountModel)); }
            var account = _accountMapper.Map<AccountModel, Account>(accountModel);
            if (account == null) { throw new ArgumentNullException(nameof(account)); }

            _context.Entry(account).State = EntityState.Modified;            
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountFromBLL (int id)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(i => i.Id == id);
            if (account == null) { throw new ArgumentNullException(nameof(account)); }

            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }
    }
}
