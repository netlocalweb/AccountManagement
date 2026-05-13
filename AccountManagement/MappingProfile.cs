using AutoMapper;
using Entities.DTO;
using Entities.Models;

namespace AccountManagement
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TestEntityDTO, TestEntity>();

            CreateMap<BankAccount, BankAccountDTO>();

            CreateMap<CreateBankAccountDTO, BankAccount>();

            CreateMap<UpdateBankAccountDTO, BankAccount>();

            CreateMap<BankTransaction, BankTransactionDTO>();

            CreateMap<CreateBankTransactionDTO, BankTransaction>();

            CreateMap<UpdateBankTransactionDTO, BankTransaction>();
        }
    }
}