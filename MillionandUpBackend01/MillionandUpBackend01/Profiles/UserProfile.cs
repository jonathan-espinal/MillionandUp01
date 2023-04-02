using AutoMapper;
using MillionandUpBackend01.Dtos.User;
using MillionandUpBackend01.Entities;

namespace MillionandUpBackend01.Profiles {
    public class UserProfile : Profile {
        public UserProfile() {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<User, UserCreateDto>();
            CreateMap<UserCreateDto, User>();
        }
    }
}
