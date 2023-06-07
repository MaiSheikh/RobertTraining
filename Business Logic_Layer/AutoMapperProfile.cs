using AutoMapper;
using Business_Logic_Layer.Features.Account.Models;
using Data_Access_Layer.Entities;

namespace Business_Logic_Layer;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Account, AccountModel>().ReverseMap();
        CreateMap<Transaction, TransactionModel>().ReverseMap();
    }
}