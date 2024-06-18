using AutoMapper;
using BugTracker.Api.Models.Requests;
using BugTracker.Domain.Enumerables;
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
                .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.User.LastName));
            CreateMap<ProjectUserDTO, ProjectUser>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore());


            CreateMap<Project, ProjectDTO>().ReverseMap();

            CreateMap<ProjectUser, ProjectParticipant>()
                .ForMember(dest => dest.Email, o => o.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.FirstName, o => o.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, o => o.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.ProfilePictureUrl, o => o.MapFrom(src => src.User.ProfilePictureUrl));

            CreateMap<User, ProjectParticipant>();

            CreateMap<Ticket, TicketDTO>();
                
            CreateMap<TicketDTO, Ticket>()
                .ForMember(dest => dest.Author, opt => opt.Ignore())
                .ForMember(dest => dest.Assignee, opt => opt.Ignore());
            CreateMap<CreateTicketRequest, Ticket>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.ProjectId)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => StatusExtensions.ParseStatus(src.Status)))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => PriorityExtensions.ParsePriority(src.Priority)));

            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.AuthorFirstName, opt => opt.MapFrom(src => src.Author.FirstName))
                .ForMember(dest => dest.AuthorLastName, opt => opt.MapFrom(src => src.Author.LastName));
            CreateMap<CommentDTO, Comment>()
                .ForMember(dest => dest.Author, opt => opt.Ignore());
                
        }

    }
}
