using AutoMapper;
using BugTracker.Domain.Models;
using BugTracker.Domain.Models.DTOs;

namespace BugTracker.Api.Helpers
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile() 
        { 
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<ProjectUser, ProjectUserDTO>().ReverseMap();
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<Ticket, TicketDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();
        }

    }
}
