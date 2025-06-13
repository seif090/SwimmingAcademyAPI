namespace SwimmingAcademy.DTOs
{
    public class SwimmerInfoTabDto
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Site { get; set; } = string.Empty;
        public string CurrentLevel { get; set; } = string.Empty;
        public string StartLevel { get; set; } = string.Empty;
        public DateTime CreatedAtDate { get; set; }
        public string PrimaryJop { get; set; } = string.Empty;
        public string? SecondaryJop { get; set; }
        public string PrimaryPhone { get; set; } = string.Empty;
        public string? SecondaryPhone { get; set; }
        public string Club { get; set; } = string.Empty;
    }
}
