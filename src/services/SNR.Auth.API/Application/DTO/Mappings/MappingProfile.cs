using AutoMapper;
using SNR.Auth.Domain.Entities;

namespace SNR.Auth.API.Application.DTO.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, UserDTO>().ReverseMap();
        }
    }
}
