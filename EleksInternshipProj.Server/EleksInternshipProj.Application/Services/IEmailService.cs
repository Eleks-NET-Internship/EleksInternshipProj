using EleksInternshipProj.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleksInternshipProj.Application.Services
{
    public interface IEmailService
    {
      Task SendDeadlineNotificationAsync(string receiverEmail, DeadlineNotificationDTO dto);
      Task SendEmailNotificationToSpace(string receiverEmail, string spaceName, SpaceAdminNotificationDTO dto);
        
    }
}
