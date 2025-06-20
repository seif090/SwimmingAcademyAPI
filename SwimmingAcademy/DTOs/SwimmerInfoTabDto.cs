namespace SwimmingAcademy.DTOs
{
    public class SwimmerInfoTabDto
    {
        public required string FullName { get; set; }
        public required DateTime BirthDate { get; set; }
        public required string Site { get; set; }
        public required string CurrentLevel { get; set; }
        public required string StartLevel { get; set; }
        public required DateTime CreatedAtDate { get; set; }
        public required string PrimaryJop { get; set; }
        public required string SecondaryJop { get; set; }
        public required string PrimaryPhone { get; set; }
        public required string SecondaryPhone { get; set; }
        public required string Club { get; set; }
    }
}
