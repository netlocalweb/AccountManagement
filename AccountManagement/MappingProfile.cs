using AutoMapper;
using Entities.DTO;
using Entities.Models;

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
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryForCreationDto, Category>().ReverseMap();
            CreateMap<CategoryForUpdateDto , Category>().ReverseMap();

        }
    }
}
