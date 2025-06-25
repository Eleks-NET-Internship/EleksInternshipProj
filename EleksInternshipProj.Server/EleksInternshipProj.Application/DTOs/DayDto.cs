namespace EleksInternshipProj.Application.DTOs
{
    public class DayDto
    {
        public long Id { get; set; }
        public string DayName { get; set; } = null!;
        public long TimetableId { get; set; }
        public IEnumerable<EventDayDto> EventDays { get; set; } = [];
    }
}
