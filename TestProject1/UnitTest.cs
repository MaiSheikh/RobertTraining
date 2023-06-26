using AutoMapper;
using Business_Logic_Layer;
using Business_Logic_Layer.Features.Account;
using Business_Logic_Layer.Features.Account.Models;
using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace UnitTestProject;

public class AccountUnitTest
{
    private readonly ContextDb _context;
    private readonly Mapper _mapper;
    private readonly AccountBLL _bll;

    public AccountUnitTest()
    {
        var options = new DbContextOptionsBuilder<ContextDb>()
            .UseInMemoryDatabase("RobertTrainingDb1")
            .Options;
        
        _context = new ContextDb(options); 
        Seed(_context);
        
        var myProfile = new AutoMapperProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile)); 
        _mapper = new Mapper(configuration);

        _bll = new AccountBLL(_context, _mapper);        
    }

    [Fact]
    public async Task GetAllAcounts_ReturnIEnumerableOfAccountModel()
    {
        // act
        var result = await _bll.GetAllAcountsFromBLL();

        // assert
        Assert.IsAssignableFrom<IEnumerable<AccountModel>>(result);
    }

    [Fact]
    public async Task GetAccountById_ReturnAccountModel()
    {
        //arrange
        var accounts = await _context.Accounts.ToListAsync();   

        //act
        var result = await _bll.GetAccountByIdFromBLL(accounts.First().Id);

        //assert
        Assert.IsAssignableFrom<AccountModel>(result);
    }
    
    [Fact]
    public async Task GetAccountById_Should_Throw_Exception_If_Not_Exist()
    {
        //arrange
        var accounts = await _context.Accounts.ToListAsync();
        var idNotInDatabase = accounts.Last().Id + 1;

        //assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _bll.GetAccountByIdFromBLL(idNotInDatabase));
    }

    [Fact]
    public async Task Balance_should_be_updated_with_expected_amount()
    {
        var account = (await _context.Accounts.ToListAsync()).First();
        var oldBalance = account.Balance;
        _context.ChangeTracker.Clear();

        account.Balance += 100;
        await _bll.UpdateAccountFromBLL(_mapper.Map< Account,AccountModel>(account));

        account.Balance.Should().Be(oldBalance + 100);
    }

    private void Seed(ContextDb context)
    {
        var accounts = new[]
        {
            new Account
            {
                Balance = 100,
                CreatedTime = DateTime.Now,
                ReferenceId = new Guid()
            },
            new Account
            {
                Balance = 100,
                CreatedTime = DateTime.Now,
                ReferenceId = new Guid()
            }
        };
            
        context.Accounts.AddRange(accounts);
        context.SaveChanges();
    }
}