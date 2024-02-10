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
            Comment? comment = await DbContext.Comments.Include(c => c.Author).FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return NotFound();
            }

            CommentDTO commentDTO = Mapper.Map<CommentDTO>(comment);
            commentDTO.AuthorFirstName = comment.Author.FirstName;
            commentDTO.AuthorLastName = comment.Author.LastName;

            return Ok(Mapper.Map<CommentDTO>(comment));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Comment>? comments = await DbContext.Comments.ToListAsync();

            List<CommentDTO> commentDTOs = comments.Select(c => new CommentDTO
            {
                Id = c.Id,
                Text = c.Text,
                AuthorId = c.Author.Id,
                AuthorFirstName = c.Author.FirstName,
                AuthorLastName = c.Author.LastName,
                TicketId = c.Ticket.Id,
                DateSubmitted = c.DateSubmitted
            }).ToList();

            return Ok(commentDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CommentDTO commentDTO)
        {
            Comment comment = new Comment
            {
                Text = commentDTO.Text,
                AuthorId = commentDTO.AuthorId,
                TicketId = commentDTO.TicketId,
                DateSubmitted = commentDTO.DateSubmitted
            };

            EntityEntry<Comment> newComment = await DbContext.Comments.AddAsync(comment);
            await DbContext.SaveChangesAsync();

            CommentDTO commentDTOToReturn = await MapToDTO(newComment.Entity.Id);

            return Created($"~/api/Comments/{commentDTO.Id}", commentDTOToReturn);
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

            CommentDTO commentDTOToReturn = await MapToDTO(updatedComment.Entity.Id);

            return Ok(commentDTOToReturn);
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

        private async Task<CommentDTO> MapToDTO(int id)
        {
            Comment? comment = await DbContext.Comments.Include(t => t.Author).FirstOrDefaultAsync(t => t.Id == id);
            if (comment == null)
                throw new Exception("There was an issue mapping the comment to a data transfer object.");

            CommentDTO? commentDTO = Mapper.Map<CommentDTO>(comment);
            commentDTO.AuthorFirstName = comment.Author.FirstName;
            commentDTO.AuthorLastName = comment.Author.LastName;

            return commentDTO;
        }
    }
}
