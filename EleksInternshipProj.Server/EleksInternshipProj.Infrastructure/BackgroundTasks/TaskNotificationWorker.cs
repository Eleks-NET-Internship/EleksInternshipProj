using Microsoft.Extensions.Hosting;

using EleksInternshipProj.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using EleksInternshipProj.Domain.Models;
using Microsoft.AspNetCore.SignalR;
using EleksInternshipProj.Infrastructure.Hubs;
using EleksInternshipProj.Application.DTOs;

namespace EleksInternshipProj.Infrastructure.BackgroundTasks
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
                // change to a reasonable delay later
                // take into account size of scanning window, so that its size is bigger than delay
            }
        }

        private async Task ProcessTaskNotificationAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            ITaskRepository taskService = scope.ServiceProvider.GetRequiredService<ITaskRepository>();
            INotificationRepository notificationRepository = scope.ServiceProvider.GetRequiredService<INotificationRepository>();

            IHubContext<NotificationHub> hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<NotificationHub>>();

            DateTime current = DateTime.UtcNow;

            // testing reminders for 24 hours before deadline
            DateTime begin = current.AddDays(1).AddMinutes(-6);
            DateTime end = current.AddDays(1).AddMinutes(6);

            // all tasks, even 'done'. We don't have standard statuses yet
            IEnumerable<TaskModel> tasks = await taskService.GetByTimePeriodAsync(begin, end);

            foreach (TaskModel task in tasks)
            {
                // may move task extraction and filtering to a service
                if (await notificationRepository.ExitstForRelatedAsync("task", task.Id))
                {
                    continue;
                }

                Notification notification = new Notification
                {
                    Id = 0,
                    Title = "Дедлайн близько!", // Shoud title and message be purely client-side?
                    Message = $"Завдання '{task.Name}' має дедлайн!",
                    RelatedType = "task",
                    RelatedId = task.Id,
                    SpaceId = task.Event.SpaceId,
                    DeadlineAt = task.EventTime
                };
                await notificationRepository.AddNotificationAsync(notification);

                Console.Write("PUSHED\nPUSHED\nPUSHED");

                // send notif via something

                // signalR
                await hubContext.Clients.All.SendAsync("ReceiveNotification",
                    new NotificationDTO
                    {
                        Title = notification.Title,
                        Message = notification.Message,
                        RelatedType = notification.RelatedType,
                        RelatedId = notification.RelatedId,
                        SpaceId = notification.SpaceId,
                        SentAt = notification.SentAt,
                        DeadlineAt = notification.DeadlineAt,
                        Read = notification.Read
                    }
                );
                // email

            }
        }
    }
}
