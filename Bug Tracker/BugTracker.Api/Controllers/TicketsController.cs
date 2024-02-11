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
    public class TicketsController : Controller
    {
        private readonly BugTrackerDbContext DbContext;
        private readonly IMapper Mapper;

        public TicketsController(BugTrackerDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Ticket? ticket = await DbContext.Tickets.Include(t => t.Author).Include(t => t.Assignee).FirstOrDefaultAsync(t => t.Id  == id);

            if (ticket == null)
            {
                return NotFound("No ticket with this id was found.");
            }

            TicketDTO ticketDTO = await MapToDTO(id);
            return Ok(ticketDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Ticket>? tickets = await DbContext.Tickets.Include(t => t.Author).Include(t => t.Assignee).ToListAsync();
            List<TicketDTO>? ticketDTOs = Mapper.Map<List<TicketDTO>>(tickets);

            return Ok(ticketDTOs);
        }

        [HttpGet]
        [Route("{ticketId:int}/Comments")]
        public async Task<IActionResult> GetAllCommentsOnTicket([FromRoute] int ticketId)
        {
            Ticket? ticket = await DbContext.Tickets.Include(t => t.Comments).ThenInclude(c => c.Author).FirstOrDefaultAsync(t => t.Id == ticketId);
            if(ticket == null)
            {
                return NotFound("No ticket with this id was found.");
            }

            List<CommentDTO> comments = ticket.Comments.Select(c => new CommentDTO
            {
                Id = c.Id,
                Text = c.Text,
                AuthorId = c.AuthorId,
                AuthorFirstName = c.Author.FirstName,
                AuthorLastName = c.Author.LastName,
                TicketId = ticketId,
                DateSubmitted = c.DateSubmitted
            }).ToList();

            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TicketDTO ticketDTO)
        {
            Ticket ticket = new Ticket
            {
                Title = ticketDTO.Title,
                Description = ticketDTO.Description,
                ProjectId = ticketDTO.ProjectId,
                AuthorId = ticketDTO.AuthorId,
                AssigneeId = ticketDTO.AssigneeId,
                Status = ticketDTO.Status,
                Priority = ticketDTO.Priority,
                TicketType = ticketDTO.TicketType,
                DateSubmitted = ticketDTO.DateSubmitted,
            };
            EntityEntry<Ticket> updatedTicket = await DbContext.Tickets.AddAsync(ticket);
            await DbContext.SaveChangesAsync();

            TicketDTO updatedTicketDTO = await MapToDTO(updatedTicket.Entity.Id);

            return Created($"~/api/Tickets/{ticketDTO.Id}", updatedTicketDTO);
        }

        [HttpPut]
        [Route("{ticketId:int}")]
        public async Task<IActionResult> Update([FromRoute] int ticketId, [FromBody] TicketDTO ticketDTO)
        {
            Ticket? ticket = await DbContext.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
            if (ticket == null)
                return NotFound();

            ticket.Title = ticketDTO.Title;
            ticket.Description = ticketDTO.Description;
            ticket.ProjectId = ticketDTO.ProjectId;
            ticket.AssigneeId = ticketDTO.AssigneeId;
            ticket.Status = ticketDTO.Status;
            ticket.Priority = ticketDTO.Priority;
            ticket.TicketType = ticketDTO.TicketType;

            DbContext.Tickets.Update(ticket);
            await DbContext.SaveChangesAsync();

            ticket = await DbContext.Tickets.Include(t => t.Author).Include(t => t.Assignee).FirstOrDefaultAsync(t => t.Id == ticketId);

            TicketDTO updatedTicket = await MapToDTO(ticketId);

            return Ok(updatedTicket);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Ticket? ticket = await DbContext.Tickets.FirstOrDefaultAsync(t => t.Id == id);
            if (ticket == null)
                return NotFound();

            DbContext.Tickets.Remove(ticket);
            await DbContext.SaveChangesAsync();

            return Ok("Ticket was successfully deleted.");
        }

        private async Task<TicketDTO> MapToDTO(int id)
        {
            Ticket? ticket = await DbContext.Tickets.Include(t => t.Author).Include(t => t.Assignee).FirstOrDefaultAsync(t => t.Id == id);
            if (ticket == null)
                throw new Exception("There was an issue mapping the ticket to a data transer object.");

            TicketDTO? ticketDTO = Mapper.Map<TicketDTO>(ticket);
            ticketDTO.AuthorFirstName = ticket.Author.FirstName;
            ticketDTO.AuthorLastName = ticket.Author.LastName;
            ticketDTO.AssigneeFirstName = ticket.Assignee != null ? ticket.Assignee.FirstName : string.Empty;
            ticketDTO.AssigneeLastName = ticket.Assignee != null ? ticket.Assignee.LastName : string.Empty;

            return ticketDTO;
        }
    }
}
