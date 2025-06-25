using EleksInternshipProj.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EleksInternshipProj.Application.DTOs;
using EleksInternshipProj.Infrastructure.Configuration;
using EleksInternshipProj.Domain.Models;

namespace EleksInternshipProj.Infrastructure.Services
{

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(EmailSettings settings)
        {
            _settings = settings;
        }
        public async Task SendDeadlineNotificationAsync(string receiverEmail, DeadlineNotificationDTO dto)
        {
            try
            {
                var smtpClient = new SmtpClient(_settings.SmtpHost, _settings.SmtpPort)
                {
                    Credentials = new NetworkCredential(_settings.SmtpUser, _settings.SmtpPassword),
                    EnableSsl = true
                };

                var message = new MailMessage
                {
                    From = new MailAddress(_settings.SmtpUser, "Navchayko"),
                    Subject = dto.Title,
                    Body = BuildDeadlineEmailBody(dto),
                    IsBodyHtml = false
                };
                message.To.Add(receiverEmail);

             

                await smtpClient.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task SendEmailNotificationToSpace(string receiverEmail, string spaceName, SpaceAdminNotificationDTO dto)
        {
            try
            {
                var smtpClient = new SmtpClient(_settings.SmtpHost, _settings.SmtpPort)
                {
                    Credentials = new NetworkCredential(_settings.SmtpUser, _settings.SmtpPassword),
                    EnableSsl = true
                };

                var message = new MailMessage
                {
                    From = new MailAddress(_settings.SmtpUser, "Navchayko"),
                    Subject = dto.Title,
                    Body = BuildSpaceAdminEmailBody(dto, spaceName),
                    IsBodyHtml = false
                };
                message.To.Add(receiverEmail);



                await smtpClient.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private string BuildDeadlineEmailBody(DeadlineNotificationDTO dto)
        {
            var kyivZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"); // Windows (Kyiv)
            var deadlineLocal = TimeZoneInfo.ConvertTimeFromUtc(dto.DeadlineAt, kyivZone);
            var sentAtLocal = TimeZoneInfo.ConvertTimeFromUtc(dto.SentAt, kyivZone);
            var sb = new StringBuilder();

            sb.AppendLine("🔔 НАГАДУВАННЯ ПРО ДЕДЛАЙН ЗАВДАННЯ");
            sb.AppendLine(new string('-', 40));
            sb.AppendLine($"{dto.Message}");
            sb.AppendLine();
            sb.AppendLine($"⏰ Дедлайн буде:   {deadlineLocal:dd.MM.yyyy HH:mm}");
            sb.AppendLine($"📅 Повідомлення надіслано: {sentAtLocal:dd.MM.yyyy HH:mm}");
            sb.AppendLine(new string('-', 40));
            sb.AppendLine("Це автоматичне повідомлення від платформи Navchayko.");

            return sb.ToString();
        }

        private string BuildSpaceAdminEmailBody(SpaceAdminNotificationDTO dto, string spaceName)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"=== Повідомлення адміністратора простору '{spaceName}' ===");
            sb.AppendLine();
            sb.AppendLine($"{dto.Message}");
            sb.AppendLine();
            sb.AppendLine("Це автоматичне повідомлення, будь ласка, не відповідайте на нього.");

            return sb.ToString();
        }

    }
}
