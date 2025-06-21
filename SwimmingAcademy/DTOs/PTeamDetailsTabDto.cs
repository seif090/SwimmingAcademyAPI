namespace SwimmingAcademy.DTOs
{
    public class PTeamDetailsTabDto
    {
        public string FullName { get; set; } = string.Empty;
        public string FirstDay { get; set; } = string.Empty;
        public string SecondDay { get; set; } = string.Empty;
        public string ThirdDay { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public bool IsEnded { get; set; }
    }
}
