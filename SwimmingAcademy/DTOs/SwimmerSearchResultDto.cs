namespace SwimmingAcademy.DTOs
{
    public class SwimmerSearchResultDto
    {
        public required string FullName { get; set; }
        public required string Year { get; set; }
        public required string CurrentLevel { get; set; }
        public required string CoachName { get; set; }
        public required string Site { get; set; }
        public required string Club { get; set; }
    }
}
