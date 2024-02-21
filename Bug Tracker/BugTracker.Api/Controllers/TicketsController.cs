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
        [Route("{ticketId:int}/Comments")]
        public async Task<IActionResult> GetAllCommentsOnTicket([FromRoute] int ticketId)
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
        public async Task<IActionResult> Create([FromBody] TicketDTO ticketDTO)
        {
            Ticket? ticket = Mapper.Map<Ticket>(ticketDTO);

            EntityEntry<Ticket> updatedTicket = await DbContext.Tickets.AddAsync(ticket);
            await DbContext.SaveChangesAsync();

            ticket = await DbContext.Tickets.Include(t => t.Author).Include(t => t.Assignee).FirstOrDefaultAsync(t => t.Id == updatedTicket.Entity.Id);
            TicketDTO? newTicketDTO = Mapper.Map<TicketDTO>(ticket);
            
            return Created($"~/api/Tickets/{updatedTicket.Entity.Id}", newTicketDTO);
        }

        [HttpPut]
        [Route("{ticketId:int}")]
        public async Task<IActionResult> Update([FromRoute] int ticketId, [FromBody] TicketDTO ticketDTO)
        {
            Ticket? ticket = await DbContext.Tickets.Include(t => t.Author).Include(t => t.Assignee).FirstOrDefaultAsync(t => t.Id == ticketId);
            if (ticket == null)
                return NotFound();

            Mapper.Map(ticketDTO, ticket);
            await DbContext.SaveChangesAsync();

            ticket = await DbContext.Tickets.Include(t => t.Author).Include(t => t.Assignee).FirstOrDefaultAsync(t => t.Id == ticketId);

            TicketDTO? updatedTicket = Mapper.Map<TicketDTO>(ticket);

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

    }
}
