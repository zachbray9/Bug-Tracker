using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Database;

namespace BugTracker.Domain.Services.Api
{
    public interface ICommentApiService : IReadable<CommentDTO>, IWritable<CommentDTO>, IUpdatable<CommentDTO>, IDeletable<CommentDTO>
    {
    }
}
