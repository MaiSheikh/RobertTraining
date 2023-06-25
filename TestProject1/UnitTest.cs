
using Xunit;
using Business_Logic_Layer.Features.Account;
using Data_Access_Layer.Data;
using AutoMapper;
using FluentAssertions;
using Business_Logic_Layer.Features.Account.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Business_Logic_Layer;
using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace UnitTestProject
{
    public class AccountUnitTest
    {



        [Fact]
        public async Task GetAllAcounts_ReturnIEnumerableOfAccountModel()
        {
            //arrange


            var options = new DbContextOptionsBuilder<ContextDb>().UseInMemoryDatabase("RobertTrainingDb1").Options;
            var context = new ContextDb(options);

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            var _bll = new Business_Logic_Layer.Features.Account.AccountBLL(context, mapper);
            Seed(context);

            //act

            var result = await _bll.GetAllAcountsFromBLL();
            //assert

            context.Accounts.Should().HaveCount(2);

            Assert.IsAssignableFrom<IEnumerable<AccountModel>>(result);

        }

        [Fact]
        public async Task GetAccountById_ReturnAccountModel()
        {
            //arrange

            int id = 4;
            var options = new DbContextOptionsBuilder<ContextDb>().UseInMemoryDatabase("RobertTrainingDb1").Options;

            var context = new ContextDb(options);

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            var _bll = new Business_Logic_Layer.Features.Account.AccountBLL(context, mapper);
            Seed(context);

            //act
            var result = await _bll.GetAccountByIdFromBLL(id);
            //assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<AccountModel>(result);

        }

        [Fact]
        public async Task UpdateAccountById_ReturnVoid()
        {
            //arrange

           
            var options = new DbContextOptionsBuilder<ContextDb>().UseInMemoryDatabase("RobertTrainingDb1").Options;


            var context = new ContextDb(options);

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            var _bll = new Business_Logic_Layer.Features.Account.AccountBLL(context, mapper);
            //act
            Seed(context);
            Account updatedAccount = new Account
            {
                Id = 2,
                Balance = 100,
                CreatedTime = DateTime.Now,
                ReferenceId = new Guid()
            };
            // context.Accounts.Update(updatedAccount);   
            var account = mapper.Map< Data_Access_Layer.Entities.Account,AccountModel> (updatedAccount);
          
            await _bll.UpdateAccountFromBLL(account);
            //assert
           

            context.Accounts.SingleOrDefaultAsync(i => i.Id == 2).Should().Be(updatedAccount);

        }

        private void Seed(ContextDb context)
        {
            var accounts = new[]
            {
                new Account
                {
                    Id = 2,
                    Balance = 1100,
                    CreatedTime = DateTime.Now,
                    ReferenceId = new Guid()
                },
                new Account
                {

                    Id = 4,
                    Balance = 22100,
                    CreatedTime = DateTime.Now,
                    ReferenceId = new Guid()
                }



            };
            context.Accounts.AddRange(accounts);
            context.SaveChanges();

        }
    }
}