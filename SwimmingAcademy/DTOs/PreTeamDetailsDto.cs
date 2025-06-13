namespace SwimmingAcademy.DTOs
{
    public class PreTeamDetailsDto
    {
        public string FullName { get; set; }
        public string FirstDay { get; set; }
        public string SecondDay { get; set; }
        public string ThirdDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool ISEnded
        {
            get; set;
        }
    }
}
