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
            CreateMap<ClientForCreationDto, Client>();
            CreateMap<Client, ClientDto>();
            CreateMap<CurrencyForCreationDto, Currency>();
            CreateMap<Currency, CurrencyDto>();
            CreateMap<BankAccountForCreationDto, BankAccount>();
            CreateMap<BankAccountForUpdateDto, BankAccount>();
            CreateMap<BankAccount, BankAccountDto>();
            CreateMap<BankTransactionForCreationDto, BankTransaction>();
            CreateMap<BankTransactionForUpdateDto, BankTransaction>();
            CreateMap<BankTransaction, BankTransactionDto>();
        }
    }
}
