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
            CreateMap<ProjectUser, ProjectUserDTO>()
                .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.User.LastName))
                .ReverseMap();
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<Ticket, TicketDTO>()
                .ForMember(dest => dest.AuthorFirstName, opt => opt.MapFrom(src => src.Author.FirstName))
                .ForMember(dest => dest.AuthorLastName, opt => opt.MapFrom(src => src.Author.LastName))
                .ForMember(dest => dest.AssigneeFirstName, opt => opt.MapFrom(src => src.Assignee != null ? src.Assignee.FirstName : null))
                .ForMember(dest => dest.AssigneeLastName, opt => opt.MapFrom(src => src.Assignee != null ? src.Assignee.LastName : null))
                .ReverseMap();
            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.AuthorFirstName, opt => opt.MapFrom(src => src.Author.FirstName))
                .ForMember(dest => dest.AuthorLastName, opt => opt.MapFrom(src => src.Author.LastName))
                .ReverseMap();
        }

    }
}
