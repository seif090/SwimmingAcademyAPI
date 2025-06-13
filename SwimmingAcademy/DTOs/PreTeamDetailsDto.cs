namespace SwimmingAcademy.DTOs
{
    public class PreTeamDetailsDto
    {
        public string CoachName { get; set; } = string.Empty;
        public string FirstDay { get; set; } = string.Empty;
        public string SecondDay { get; set; } = string.Empty;
        public string ThirdDay { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsEnded { get; set; }
    }
}
