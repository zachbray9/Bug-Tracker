using BugTracker.Api.Models.Requests;
using BugTracker.Api.Services.EntityServices;
using BugTracker.Domain.Models.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace BugTracker.Api.SignalR
{
    public class ChatHub : Hub
    {
        private readonly CommentService CommentService;

        public ChatHub(CommentService commentService)
        {
            CommentService = commentService;
        }

        public async Task SendComment(CreateCommentRequest request)
        {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            CommentDTO? comment = await CommentService.CreateComment(userId, request.TicketId, request.Text);

            await Clients.Group(request.TicketId.ToString())
                .SendAsync("ReceiveComment", comment);
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            string? ticketId = httpContext?.Request.Query["ticketId"];
            await Groups.AddToGroupAsync(Context.ConnectionId, ticketId!);
            List<CommentDTO> comments = await CommentService.GetAllCommentsOnTicket(Guid.Parse(ticketId!));
            await Clients.Caller.SendAsync("LoadComments", comments);
        }
    }
}
