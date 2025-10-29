using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace AccountManagement
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Client mappings
            CreateMap<Client, ClientReadDto>().ReverseMap();
            CreateMap<ClientCreateDto, Client>();
            CreateMap<ClientUpdateDto, Client>();

            //Currency mappings
            CreateMap<Currency, CurrencyReadDto>();
            CreateMap<CurrencyCreateDto, Currency>();
            CreateMap<CurrencyUpdateDto, Currency>();

            // Category mappings
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();

            // Product mappings
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductsCreateDto, Product>();

            // BankAccount mappings
            CreateMap<BankAccount, BankAccountReadDto>().ReverseMap();
            CreateMap<BankAccountCreateDto, BankAccount>();
            CreateMap<BankAccountUpdateDto, BankAccount>();

            // BankTransactions mappings
            CreateMap<BankTransaction, BankTransactionReadDto>();
            CreateMap<BankTransactionCreateDto, BankTransaction>();


        }
    }
    }
