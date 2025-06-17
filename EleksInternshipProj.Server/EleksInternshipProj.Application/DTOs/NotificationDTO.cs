namespace EleksInternshipProj.Application.DTOs
{
    public class NotificationDTO
    {

        public string Title { get; set; }

        public string Message { get; set; }

        public DateTime SentAt { get; set; }

        public string RelatedType { get; set; }

        public long RelatedId { get; set; }
    }
}
