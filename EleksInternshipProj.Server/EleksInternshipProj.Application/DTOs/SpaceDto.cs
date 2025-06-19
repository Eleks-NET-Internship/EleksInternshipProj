namespace EleksInternshipProj.Application.DTOs
{
    public class SpaceDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public List<UserSpaceDtoShort>? UserSpaces { get; set; }
    }
}


