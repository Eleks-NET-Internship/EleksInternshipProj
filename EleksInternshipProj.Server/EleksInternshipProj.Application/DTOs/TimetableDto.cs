namespace EleksInternshipProj.Application.DTOs
{
    public class TimetableDto
    {
        public long Id { get; set; }
        public long SpaceId { get; set; }
        public List<DayDto> Days { get; set; } = new();
    }
    
    public class TimetableDtoShort
    {
        public long Id { get; set; }
        public long SpaceId { get; set; }
    }
}
