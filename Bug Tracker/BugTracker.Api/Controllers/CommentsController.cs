using AutoMapper;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Models;
using BugTracker.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        private readonly BugTrackerDbContext DbContext;
        private readonly IMapper Mapper;
        private readonly IHttpContextAccessor HttpContextAccessor;

        public CommentsController(BugTrackerDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            DbContext = dbContext;
            Mapper = mapper;
            HttpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Comment>? comments = await DbContext.Comments.Include(c => c.Author).Include(c => c.Ticket).ToListAsync();
            List<CommentDTO>? commentDTOs = Mapper.Map<List<CommentDTO>>(comments);
            return Ok(commentDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CommentDTO commentDTO)
        {
            Comment? comment = new Comment();
            comment = Mapper.Map<Comment>(commentDTO);

            EntityEntry<Comment> newComment = await DbContext.Comments.AddAsync(comment);
            await DbContext.SaveChangesAsync();

            comment = await DbContext.Comments.Include(c => c.Author).FirstOrDefaultAsync(c => c.Id == newComment.Entity.Id);
            CommentDTO? newCommentDTO = Mapper.Map<CommentDTO>(comment);

            return Created($"~/api/Comments/{newComment.Entity.Id}", newCommentDTO);
        }

        [HttpPut]
        [Route("commentId:guid")]
        public async Task<IActionResult> Update([FromRoute] Guid commentId, [FromBody] CommentDTO commentDTO)
        {
            Comment? comment = await DbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            if (comment == null)
                return NotFound();

            Mapper.Map(commentDTO, comment);
            await DbContext.SaveChangesAsync();

            comment = await DbContext.Comments.Include(c => c.Author).FirstOrDefaultAsync(c => c.Id == commentId);
            CommentDTO? updatedComment = Mapper.Map<CommentDTO>(comment);

            return Ok(updatedComment);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Comment? comment = await DbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null)
                return NotFound();

            DbContext.Comments.Remove(comment);
            await DbContext.SaveChangesAsync();

            return Ok("Comment was successfully deleted.");
        }
    }
}
