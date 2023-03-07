using AutoMapper;
using TaskManagerAPI.Dtos;
using TaskManagerDomain.Models;

namespace TaskManagerAPI.Mapper
{
    public class UserMappingProfiles: Profile
    {
        public UserMappingProfiles()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
