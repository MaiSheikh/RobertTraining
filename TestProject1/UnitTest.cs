
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
using System.Security.Cryptography.X509Certificates;
using Business_Logic_Layer.Features.Transaction;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;

namespace UnitTestProject
{
    public class AccountUnitTest
    {
        DbContextOptions<ContextDb> options;
        ContextDb context;
        AutoMapperProfile myProfile;
        MapperConfiguration configuration;
        Mapper mapper;
       AccountBLL _bll;

        public AccountUnitTest()
        {
           // options = new DbContextOptionsBuilder<ContextDb>().UseInMemoryDatabase("BloggingControllerTest").ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;
              options = new DbContextOptionsBuilder<ContextDb>().UseInMemoryDatabase("RobertTrainingDb1").Options;
            context = new ContextDb(options);

            myProfile = new AutoMapperProfile();
             configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
             mapper = new Mapper(configuration);

           _bll = new AccountBLL(context, mapper);        
        }

        [Fact]
        public async Task GetAllAcounts_ReturnIEnumerableOfAccountModel()
        {
            //arrange
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
            Seed(context);

            //act
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