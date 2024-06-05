using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Demo.PL.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<ApplicationUser,UserViewModel>().ReverseMap();
            CreateMap<IdentityRole, RoleViewModel>()
                .ForMember(d => d.RoleName,o=>o.MapFrom(s=>s.Name)).ReverseMap();
            
        }
    }
}
