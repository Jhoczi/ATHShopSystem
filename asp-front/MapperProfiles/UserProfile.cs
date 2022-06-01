using asp_front.Models.ViewModels.User;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace asp_front.MapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserViewModel, IdentityUser>().ReverseMap();
    }
}