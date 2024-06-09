using AutoMapper;
using IdentiryServer.DTOs;
using IdentiryServer.Entities;

namespace IdentiryServer.Mapper
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<User, NewUserDTO>().ReverseMap();
        }
    }
}
