namespace EleksInternshipProj.Application.DTOs
{
    public class ProfileDto
    {
        public string Username { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Email { get; set; }
    }

    public class UpdateProfileDto
    {
        public string Username { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
    }
}
