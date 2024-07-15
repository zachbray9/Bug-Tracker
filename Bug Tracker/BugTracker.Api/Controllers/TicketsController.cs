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
using Microsoft.AspNetCore.JsonPatch;
using BugTracker.Domain.Enumerables;

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

        [HttpPatch]
        [Route("{ticketId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid ticketId, [FromBody] JsonPatchDocument<TicketDTO> patchDoc )
        {
            if(patchDoc == null)
                return BadRequest(ModelState);

            Ticket? ticket = await DbContext.Tickets.Include(t => t.Author).Include(t => t.Assignee).FirstOrDefaultAsync(t => t.Id == ticketId);
            if (ticket == null)
                return NotFound();

            TicketDTO? ticketToPatch = Mapper.Map<TicketDTO>(ticket);

            //converts status and priority from string to enum because client sends sends them up as a string, but they are stored in the database as enums.
            foreach (var operation in patchDoc.Operations)
            {
                var path = operation.path.TrimStart('/');
                if (path == "status")
                {
                    operation.value = StatusExtensions.ParseStatus(operation.value.ToString()!);
                }
                else if (path == "priority")
                {
                    operation.value = PriorityExtensions.ParsePriority(operation.value.ToString()!);
                }
            }

            patchDoc.ApplyTo(ticketToPatch!, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Mapper.Map(ticketToPatch, ticket);

            await DbContext.SaveChangesAsync();

            TicketDTO? updatedTicket = await DbContext.Tickets.ProjectTo<TicketDTO>(Mapper.ConfigurationProvider).FirstOrDefaultAsync(t => t.Id == ticketId);

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
