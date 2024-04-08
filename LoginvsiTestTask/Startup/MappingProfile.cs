using AutoMapper;
using LoginvsiTestTask.DTOs;
using LoginvsiTestTask.Models;

namespace LoginvsiTestTask.Startup;

/// <summary>
/// AutoMapper profile
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserDTO, User>()
            .ForMember(dest=>dest.Id, 
                opt=>opt.MapFrom(src=>Guid.NewGuid()));
        CreateMap<User, UserDTO>();
        CreateMap<CreateAssignmentDTO, Assignment>()
            .ForMember(dest=>dest.Id, 
                opt=>opt.MapFrom(src=>Guid.NewGuid()));
        CreateMap<Assignment, AssignmentDTO>();
    }
}