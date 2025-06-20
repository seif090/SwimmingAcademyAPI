namespace SwimmingAcademy.DTOs
{
    public class SwimmerTechnicalPreTeamDto
    {
        public required string CoachName { get; set; }
        public required string FirstDay { get; set; }
        public required string SecondDay { get; set; }
        public required string ThirdDay { get; set; }
        public required TimeSpan StartTime { get; set; }
        public required TimeSpan EndTime { get; set; }
        public required string SwimmerLevel { get; set; }
        public required string Attendence { get; set; }
        public required string LastStar { get; set; }
    }
}
