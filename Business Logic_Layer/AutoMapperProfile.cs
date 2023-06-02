using AutoMapper;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountModel>().ReverseMap();
            CreateMap<Transaction, TransactionModel>().ReverseMap();
        }
    }
}
