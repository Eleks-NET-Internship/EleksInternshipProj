using Microsoft.Extensions.Hosting;

using EleksInternshipProj.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using EleksInternshipProj.Domain.Models;
using EleksInternshipProj.Application.Services;
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
                await ProcessTaskNotificationAsync(24 * 60);
                await ProcessTaskNotificationAsync(1 * 60);

                await Task.Delay(TimeSpan.FromSeconds(4), stoppingToken);   // THIS NOT FOR PROD
                //await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // THIS FOR PROD
            }
        }

        private async Task ProcessTaskNotificationAsync(int minutes)
        {
            if (minutes < 1)
            {
                throw new ArgumentException($"{minutes} minutes in the future isn't valid");
            }

            using var scope = _serviceProvider.CreateScope();
            ITaskRepository taskService = scope.ServiceProvider.GetRequiredService<ITaskRepository>();
            INotificationRepository notificationRepository = scope.ServiceProvider.GetRequiredService<INotificationRepository>();
            INotificationDeliveryService notificationDeliveryService = scope.ServiceProvider.GetRequiredService<INotificationDeliveryService>();
            IEmailService emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
            IUserSpaceRepository userSpaceService = scope.ServiceProvider.GetRequiredService<IUserSpaceRepository>();

            DateTime current = DateTime.UtcNow;

            DateTime begin = current.AddMinutes(minutes).AddMinutes(-6);
            DateTime end = current.AddMinutes(minutes).AddMinutes(6);

            // all tasks without notifications. However, tasks' status isn't checked
            IEnumerable<TaskModel> tasks = await taskService.GetByTimePeriodWithoutNotifAsync(begin, end, minutes);

            foreach (TaskModel task in tasks)
            {
                Notification notification = new Notification
                {
                    Id = 0,
                    Title = minutes < 12*60 ? "Дедлайн дуже близько!" : "Дедлайн близько",
                    Message = $"Завдання '{task.Name}' має дедлайн!",
                    NotificationType = NotificationType.Reminder,
                    RelatedType = "task",
                    RelatedId = task.Id,
                    SpaceId = task.Event.SpaceId,
                    DeadlineAt = task.EventTime,
                    SentBefore = minutes
                };
                await notificationRepository.AddNotificationAsync(notification);

                Console.Write("PUSHED\nPUSHED\nPUSHED");

                // send notif via something

                // signalR
                DeadlineNotificationDTO dto = new DeadlineNotificationDTO
                {
                    Title = notification.Title,
                    Message = notification.Message,
                    RelatedType = notification.RelatedType,
                    RelatedId = notification.RelatedId.Value,
                    SpaceId = notification.SpaceId,
                    SentAt = notification.SentAt,
                    DeadlineAt = notification.DeadlineAt.Value,
                };
                await notificationDeliveryService.SendReminderToSpaceAsync(dto);

                // email
                var users = await userSpaceService.GetUsersBySpaceId(dto.SpaceId);
                foreach (var user in users)
                {
                    await emailService.SendDeadlineNotificationAsync(user.Email, dto);
                }
            }
        }
    }
}
