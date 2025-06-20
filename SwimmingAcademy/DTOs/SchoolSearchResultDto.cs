namespace SwimmingAcademy.DTOs
{
    public class SchoolSearchResultDto
    {
        public required string CoachName { get; set; }
        public required string Level { get; set; }
        public required string Type { get; set; }
        public required string Days { get; set; }
        public required string FromTo { get; set; }
        public required string NumberCapacity { get; set; }
    }
}
