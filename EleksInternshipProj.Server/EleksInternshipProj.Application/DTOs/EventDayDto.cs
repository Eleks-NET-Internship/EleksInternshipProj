namespace EleksInternshipProj.Application.DTOs
{
    public class EventDayDto
    {
        public long Id { get; set; }
        public long EventId { get; set; }
        public long DayId { get; set; }
        public TimeOnly SatrtTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
