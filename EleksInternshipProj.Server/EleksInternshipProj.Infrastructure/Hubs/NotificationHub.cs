using System.Security.Claims;

using Microsoft.AspNetCore.SignalR;

using EleksInternshipProj.Application.Services;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Infrastructure.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly ISpaceService _spaceService;

        public NotificationHub(ISpaceService spaceService)
        {
            _spaceService = spaceService;
        }

        public async Task JoinSpaces()
        {
            string? id = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id == null)
            {
                return;
            }

            long userId = long.Parse(id);

            (IEnumerable<Space> spaces, _) = await _spaceService.GetSpacesAsync(userId);
            foreach (Space space in spaces)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"space-{space.Id}");
            }
        }
        public async Task LeaveSpaces()
        {
            // However, if space is deleted, users will still be in group until connection closes
            string? id = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id == null)
            {
                return;
            }

            long userId = long.Parse(id);

            (IEnumerable<Space> spaces, _) = await _spaceService.GetSpacesAsync(userId);
            foreach (Space space in spaces)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"space-{space.Id}");
            }
        }
    }
}
