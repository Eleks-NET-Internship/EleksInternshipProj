using Microsoft.Extensions.Hosting;

using EleksInternshipProj.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EleksInternshipProj.Application.Services.Imp
{
    public class TaskNotificationWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public TaskNotificationWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await ProcessTaskNotificationAsync();
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }

        private async Task ProcessTaskNotificationAsync()
        {
            Console.WriteLine("Ready");
            using var scope = _serviceProvider.CreateScope();
            ITaskRepository taskService = scope.ServiceProvider.GetRequiredService<ITaskRepository>();
            INotificationRepository notificationRepository = scope.ServiceProvider.GetRequiredService<INotificationRepository>();
        }
    }
}
