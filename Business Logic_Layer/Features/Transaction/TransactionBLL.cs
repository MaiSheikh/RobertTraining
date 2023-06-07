using AutoMapper;
using Business_Logic_Layer.Features.Account.Models;
using Data_Access_Layer.Data;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic_Layer.Features.Transaction;

public class TransactionBLL
{
    private readonly ContextDb _context;
    private readonly IMapper _transactionMapper;

    public TransactionBLL(ContextDb context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _transactionMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<TransactionModel>> GetAllTransactionsFromBLL()
    {
        var transactionsFromDB = await _context.Transactions.ToListAsync();
        var transactionsModel = _transactionMapper.Map<List<Data_Access_Layer.Entities.Transaction>, List<TransactionModel>>(transactionsFromDB);
        return transactionsModel;
        // return await Task.FromResult( accountsModel);
    }

    public async Task<TransactionModel> GetTransactionByIdFromBLL(int id)
    {
        var transaction = await _context.Transactions.SingleOrDefaultAsync(i => i.Id == id);
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        var transactionModel = _transactionMapper.Map<Data_Access_Layer.Entities.Transaction, TransactionModel>(transaction);

        return transactionModel;
    }

    public async Task<Data_Access_Layer.Entities.Transaction> CreateTransactionFromBLL(TransactionModel transactionModel)
    {
        var transaction = _transactionMapper.Map<TransactionModel, Data_Access_Layer.Entities.Transaction>(transactionModel);

        await _context.Transactions.AddAsync(transaction);

        var account = await _context.Accounts.SingleOrDefaultAsync(c => c.Id == transactionModel.AccountId);


        if (account == null) throw new NullReferenceException();

        account.Balance = account.Balance + transaction.Delta;

        await _context.SaveChangesAsync();

        return transaction;
    }

    public async Task DeleteTransactionFromBLL(int id)
    {
        var transaction = await _context.Transactions.SingleOrDefaultAsync(i => i.Id == id);

        if (transaction == null) throw new ArgumentNullException(nameof(transaction));

        _context.Transactions.Remove(transaction);

        var account = await _context.Accounts.SingleOrDefaultAsync(c => c.Id == transaction.AccountId);

        if (account is null) throw new NullReferenceException(nameof(account));

        account.Balance -= transaction.Delta;

        await _context.SaveChangesAsync();

            
    }
}