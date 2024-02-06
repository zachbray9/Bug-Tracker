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

        public CommentsController(BugTrackerDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Comment? comment = await DbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<CommentDTO>(comment));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Comment>? comments = await DbContext.Comments.ToListAsync();

            List<CommentDTO> commentDTOs = comments.Select(c => Mapper.Map<CommentDTO>(c)!).ToList();

            return Ok(commentDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CommentDTO commentDTO)
        {
            if (commentDTO == null)
                return BadRequest("The Comment object you are trying to pass is null.");

            Comment comment = new Comment
            {
                Text = commentDTO.Text,
                AuthorId = commentDTO.AuthorId,
                TicketId = commentDTO.TicketId,
                DateSubmitted = commentDTO.DateSubmitted
            };
            EntityEntry<Comment> newComment = await DbContext.Comments.AddAsync(comment);
            await DbContext.SaveChangesAsync();

            return Created($"~/api/Comments/{commentDTO.Id}", Mapper.Map<CommentDTO>(newComment.Entity));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CommentDTO commentDTO)
        {
            if (commentDTO == null)
                return BadRequest("The Comment object you are trying to pass is null.");

            Comment? comment = await DbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentDTO.Id);
            if (comment == null)
                return NotFound();

            comment.Text = commentDTO.Text;
            comment.AuthorId = commentDTO.AuthorId;
            comment.TicketId = commentDTO.TicketId;
            comment.DateSubmitted = commentDTO.DateSubmitted;

            EntityEntry<Comment> updatedComment = DbContext.Comments.Update(comment);
            await DbContext.SaveChangesAsync();

            return Ok(Mapper.Map<CommentDTO>(updatedComment.Entity));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
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
