namespace SwimmingAcademy.DTOs
{
    public class SchoolDetailsTabDto
    {
        public string FullName { get; set; }
        public string FirstDay { get; set; }
        public string SecondDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Capacity { get; set; }
        public int NumberOfSwimmers { get; set; }
        public bool ISEnded { get; set; }
    }
}
