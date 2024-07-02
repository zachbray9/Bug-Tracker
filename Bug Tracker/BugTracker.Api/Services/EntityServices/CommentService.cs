using AutoMapper;
using AutoMapper.QueryableExtensions;
using BugTracker.Domain.Models;
using BugTracker.Domain.Models.DTOs;
using BugTracker.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BugTracker.Api.Services.EntityServices
{
    public class CommentService
    {
        private readonly BugTrackerDbContext DbContext;
        private readonly IMapper Mapper;

        public CommentService(BugTrackerDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper; 
        }

        public async Task<CommentDTO?> CreateComment(string userId, Guid ticketId, string body)
        {
            var ticket = await DbContext.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);

            if (ticket == null) return null;

            var user = await DbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            Comment comment = new Comment
            {
                Text = body,
                AuthorId = userId,
                TicketId = ticketId,
                ProjectId = ticket.ProjectId,
                DateSubmitted = DateTime.UtcNow
            };

            EntityEntry<Comment> entity = await DbContext.Comments.AddAsync(comment);

            bool success = await DbContext.SaveChangesAsync() > 0;
            if (!success) return null;

            CommentDTO? commentDto = await DbContext.Comments.ProjectTo<CommentDTO>(Mapper.ConfigurationProvider).FirstOrDefaultAsync(c => c.Id == entity.Entity.Id);

            return commentDto;
        }

        public async Task<List<CommentDTO>> GetAllCommentsOnTicket(Guid ticketId)
        {
            var comments = await DbContext.Comments
                .Where(c => c.TicketId == ticketId)
                .OrderBy(c => c.DateSubmitted)
                .ProjectTo<CommentDTO>(Mapper.ConfigurationProvider)
                .ToListAsync();

            return comments;
        }
    }
}
