﻿namespace EleksInternshipProj.Application.DTOs
{
    public class DeadlineNotificationDTO
    {

        public string Title { get; set; }

        public string Message { get; set; }

        public string RelatedType { get; set; }

        public long RelatedId { get; set; }

        public long SpaceId { get; set; }

        public DateTime SentAt { get; set; }

        public DateTime DeadlineAt { get; set; }
    }
}
