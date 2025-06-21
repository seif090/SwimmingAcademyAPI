namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Represents detailed technical data for a swimmer in the Pre-Team.
    /// </summary>
    public class SwimmerTechnicalPreTeamDto
    {
        public string CoachName { get; set; } = string.Empty;
        public string FirstDay { get; set; } = string.Empty;
        public string SecondDay { get; set; } = string.Empty;
        public string ThirdDay { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string SwimmerLevel { get; set; } = string.Empty;
        public string Attendence { get; set; } = string.Empty;
        public string LastStar { get; set; } = string.Empty;
    }
}
