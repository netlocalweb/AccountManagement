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
        }
    }
}
