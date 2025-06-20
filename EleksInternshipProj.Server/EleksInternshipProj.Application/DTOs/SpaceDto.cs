namespace EleksInternshipProj.Application.DTOs
{
    public class SpaceDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public List<UserSpaceDto>? UserSpaces { get; set; }
        
        public TimetableDtoShort? Timetable { get; set; } = null!;
    }
    
    public class SpaceDtoShort
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public List<UserSpaceDtoShort>? UserSpaces { get; set; }
        
        public TimetableDtoShort? Timetable { get; set; } = null!;
    }

    public class SpaceRenameDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}


