namespace SwimmingAcademy.DTOs
{
    public class CreatePreTeamDto
    {
        public short PTeamLevel { get; set; }
        public int CoachID { get; set; }
        public string FirstDay { get; set; } = string.Empty;
        public string SecondDay { get; set; } = string.Empty;
        public string ThirdDay { get; set; } = string.Empty;
        public short Site { get; set; }
        public int User { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
