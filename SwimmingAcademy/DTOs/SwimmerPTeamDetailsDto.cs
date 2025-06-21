namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Represents detailed information about a swimmer in a Pre-Team.
    /// </summary>
    public class SwimmerPTeamDetailsDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Attendance { get; set; } = string.Empty;
        public string SwimmerLevel { get; set; } = string.Empty;
        public string LastStar { get; set; } = string.Empty;
    }
}
