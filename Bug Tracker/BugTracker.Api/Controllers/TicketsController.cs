using AutoMapper;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Models;
using BugTracker.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using BugTracker.Api.Models.Requests;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using AutoMapper.QueryableExtensions;

namespace BugTracker.Api.Controllers
{
    [Authorize]
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
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            Ticket? ticket = await DbContext.Tickets.Include(t => t.Author).Include(t => t.Assignee).FirstOrDefaultAsync(t => t.Id  == id);

            if (ticket == null)
            {
                return NotFound("No ticket with this id was found.");
            }

            TicketDTO? ticketDTO = Mapper.Map<TicketDTO>(ticket);
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
        [Route("{ticketId:guid}/Comments")]
        public async Task<IActionResult> GetAllCommentsOnTicket([FromRoute] Guid ticketId)
        {
            Ticket? ticket = await DbContext.Tickets.Include(t => t.Comments).ThenInclude(c => c.Author).FirstOrDefaultAsync(t => t.Id == ticketId);
            if(ticket == null)
            {
                return NotFound("No ticket with this id was found.");
            }

            List<CommentDTO>? comments = Mapper.Map<List<CommentDTO>>(ticket.Comments);

            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketRequest newTicketRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Ticket? ticket = Mapper.Map<Ticket>(newTicketRequest);
            ticket!.Id = Guid.NewGuid();
            ticket.AuthorId = userId;
            ticket.DateSubmitted = DateTime.UtcNow;

            EntityEntry<Ticket> updatedTicket = await DbContext.Tickets.AddAsync(ticket!);
            await DbContext.SaveChangesAsync();

            TicketDTO? ticketDto = await DbContext.Tickets.ProjectTo<TicketDTO>(Mapper.ConfigurationProvider).FirstOrDefaultAsync(t => t.Id == updatedTicket.Entity.Id);
            
            return Created($"~/api/Tickets/{updatedTicket.Entity.Id}", ticketDto);
        }

        [HttpPut]
        [Route("{ticketId}")]
        public async Task<IActionResult> Update([FromRoute] string ticketId, [FromBody] UpdateTicketRequest ticketDTO)
        {
            Ticket? ticket = await DbContext.Tickets.Include(t => t.Author).Include(t => t.Assignee).FirstOrDefaultAsync(t => t.Id == Guid.Parse(ticketId));
            if (ticket == null)
                return NotFound();

            Mapper.Map(ticketDTO, ticket);
            await DbContext.SaveChangesAsync();

            TicketDTO? updatedTicket = await DbContext.Tickets.ProjectTo<TicketDTO>(Mapper.ConfigurationProvider).FirstOrDefaultAsync(t => t.Id == Guid.Parse(ticketId));

            return Ok(updatedTicket);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Ticket? ticket = await DbContext.Tickets.FirstOrDefaultAsync(t => t.Id == id);
            if (ticket == null)
                return NotFound();

            DbContext.Tickets.Remove(ticket);
            await DbContext.SaveChangesAsync();

            return Ok("Ticket was successfully deleted.");
        }

    }
}
