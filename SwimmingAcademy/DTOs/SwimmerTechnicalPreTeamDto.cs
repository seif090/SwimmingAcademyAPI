namespace SwimmingAcademy.DTOs
{
    public class SwimmerTechnicalPreTeamDto
    {
        public string CoachName { get; set; }
        public string FirstDay { get; set; }
        public string SecondDay { get; set; }
        public string ThirdDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string SwimmerLevel { get; set; }
        public string Attendence { get; set; }
        public string LastStar { get; set; }
    }
}
