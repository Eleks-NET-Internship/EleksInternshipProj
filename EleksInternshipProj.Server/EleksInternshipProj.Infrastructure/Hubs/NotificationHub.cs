using System.Security.Claims;
using EleksInternshipProj.Application.Mappers;
using Microsoft.AspNetCore.SignalR;

using EleksInternshipProj.Application.Services;
using EleksInternshipProj.Domain.Models;
using System.Collections.Concurrent;

namespace EleksInternshipProj.Infrastructure.Hubs
{
    public class NotificationHub : Hub
    {
        private static readonly ConcurrentDictionary<long, HashSet<string>> UserConnections = new();

        private readonly ISpaceService _spaceService;

        public NotificationHub(ISpaceService spaceService)
        {
            _spaceService = spaceService;
        }

        public override Task OnConnectedAsync()
        {
            string? userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null && long.TryParse(userId, out var parsedId))
            {
                HashSet<string> connections = UserConnections.GetOrAdd(parsedId, _ => new HashSet<string>());
                lock (connections)
                {
                    connections.Add(Context.ConnectionId);
                }
            }
            
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            string? userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null && long.TryParse(userId, out var parsedId))
            {
                if (UserConnections.TryGetValue(parsedId, out HashSet<string>? connections))
                {
                    lock (connections)
                    {
                        connections.Remove(Context.ConnectionId);
                        if (connections.Count == 0)
                        {
                            UserConnections.TryRemove(parsedId, out _);
                        }
                    }
                }
            }
            return base.OnDisconnectedAsync(exception);
        }

        public static IReadOnlyCollection<string> GetUserConnections(long userId)
        {
            return UserConnections.TryGetValue(userId, out var connections) ? connections.ToList() : Array.Empty<string>();
        }

        public async Task JoinSpaces()
        {
            string? id = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id == null)
            {
                return;
            }

            long userId = long.Parse(id);

            IEnumerable<Space> spaces = (await _spaceService.GetSpacesAsync(userId)).Select(s => s.ToEntity());
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

            IEnumerable<Space> spaces = (await _spaceService.GetSpacesAsync(userId)).Select(s => s.ToEntity());
            foreach (Space space in spaces)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"space-{space.Id}");
            }
        }
    }
}
