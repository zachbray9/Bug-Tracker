﻿using AutoMapper;
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

            CreateMap<Ticket, TicketDTO>()
                .ForMember(dest => dest.AuthorFirstName, opt => opt.MapFrom(src => src.Author.FirstName))
                .ForMember(dest => dest.AuthorLastName, opt => opt.MapFrom(src => src.Author.LastName))
                .ForMember(dest => dest.AssigneeFirstName, opt => opt.MapFrom(src => src.Assignee != null ? src.Assignee.FirstName : null))
                .ForMember(dest => dest.AssigneeLastName, opt => opt.MapFrom(src => src.Assignee != null ? src.Assignee.LastName : null));
            CreateMap<TicketDTO, Ticket>()
                .ForMember(dest => dest.Author, opt => opt.Ignore())
                .ForMember(dest => dest.Assignee, opt => opt.Ignore());

            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.AuthorFirstName, opt => opt.MapFrom(src => src.Author.FirstName))
                .ForMember(dest => dest.AuthorLastName, opt => opt.MapFrom(src => src.Author.LastName));
            CreateMap<CommentDTO, Comment>()
                .ForMember(dest => dest.Author, opt => opt.Ignore());
                
        }

    }
}