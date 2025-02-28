using AutoMapper;
using Entities.DTO;
using Entities.Models;

namespace AccountManagement
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<Client, ClientRegisterDTO>().ReverseMap();
            CreateMap<Client, ClientLoginDTO>().ReverseMap();

            CreateMap<BankAccount, BankAccountDTO>().ReverseMap();
            CreateMap<BankAccount, CreateBankAccDTO>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, AddCategoryDTO>().ReverseMap();

            CreateMap<Currency, CurrencyDTO>().ReverseMap();
            CreateMap<Currency, AddCurrencyDTO>().ReverseMap();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, CreateProductDTO>().ReverseMap();

            CreateMap<BankTransaction, BankTransactionDTO>().ReverseMap();


        }
    }
}


