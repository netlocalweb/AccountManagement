using AutoMapper;
using Entities.DTO;
using Entities.Enums;
using Entities.Models;
using Microsoft.OpenApi.Writers;

namespace AccountManagement
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
          
            CreateMap<UserForRegistrationDto, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<UpdateClientDto, Client>();

            CreateMap<Currency, CurrencyDto>().ReverseMap();
            CreateMap<CurrencyCreationDto , Currency>().ReverseMap();
            CreateMap<CurrencyUpdateDto,Currency>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryForCreationDto, Category>().ReverseMap();
            CreateMap<CategoryForUpdateDto , Category>().ReverseMap();

            CreateMap<Products, ProductsDto>().ReverseMap();
            CreateMap<ProductForCreationDto , Products>().ReverseMap();
            CreateMap<ProductForUpdateDto , Products>().ReverseMap();

            CreateMap<BankAccount, BankAccountDto>().ReverseMap();
            CreateMap<BankAccountForCreationDto , BankAccount>().ReverseMap();
            CreateMap<BankAccountForUpdateDto, BankAccount>().ReverseMap();

            CreateMap<BankTransaction, BankTransactionDto>().ReverseMap()
            .ForMember(dest =>dest.Action,opt => opt.MapFrom(src =>src.Action.ToString()));
            CreateMap<BankTransactionForCreation, BankTransaction>().ReverseMap()
            .ForMember(dest => dest.Action, opt => opt.MapFrom(src => (TransactionAction)src.Action));


        }
    }
}
