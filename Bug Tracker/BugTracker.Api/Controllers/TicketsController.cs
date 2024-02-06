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
            Ticket? ticket = await DbContext.Tickets
                .Include(t => t.Comments)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<TicketDTO>(ticket));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Ticket>? tickets = await DbContext.Tickets
                .Include(t => t.Comments)
                .ToListAsync();

            List<TicketDTO> ticketDTOs = tickets.Select(t => Mapper.Map<TicketDTO>(t)!).ToList();

            return Ok(ticketDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TicketDTO ticketDTO)
        {
            if (ticketDTO == null)
                return BadRequest("The ticket object you are trying to pass is null.");

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
            EntityEntry<Ticket> newTicket = await DbContext.Tickets.AddAsync(ticket);
            await DbContext.SaveChangesAsync();

            return Created($"~/api/Tickets/{ticketDTO.Id}", Mapper.Map<TicketDTO>(newTicket.Entity));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TicketDTO ticketDTO)
        {
            if (ticketDTO == null)
                return BadRequest("The Ticket object you are trying to pass is null.");

            Ticket? ticket = await DbContext.Tickets.FirstOrDefaultAsync(t => t.Id == ticketDTO.Id);
            if (ticket == null)
                return NotFound();

            ticket.Id = ticketDTO.Id;
            ticket.Title = ticketDTO.Title;
            ticket.Description = ticketDTO.Description;
            ticket.ProjectId = ticketDTO.ProjectId;
            ticket.AuthorId = ticketDTO.AuthorId;
            ticket.AssigneeId = ticketDTO.AssigneeId;
            ticket.Status = ticketDTO.Status;
            ticket.Priority = ticketDTO.Priority;
            ticket.TicketType = ticketDTO.TicketType;

            EntityEntry<Ticket> updatedTicket = DbContext.Tickets.Update(ticket);
            await DbContext.SaveChangesAsync();

            return Ok(Mapper.Map<TicketDTO>(updatedTicket.Entity));
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
