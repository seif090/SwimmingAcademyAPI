namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Request DTO for creating a new Pre-Team training group.
    /// </summary>
    public class CreatePTeamRequest
    {
        public short PreTeamLevel { get; set; }
        public int CoachID { get; set; }
        public string? FirstDay { get; set; }
        public string? SecondDay { get; set; }
        public string? ThirdDay { get; set; }
        public short Site { get; set; }
        public int User { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
