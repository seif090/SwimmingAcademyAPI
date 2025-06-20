namespace SwimmingAcademy.DTOs
{
    public class SchoolDetailsTabDto
    {
        public string FullName { get; set; } = string.Empty;
        public string FirstDay { get; set; } = string.Empty;
        public string SecondDay { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string NumberOfSwimmers { get; set; } = string.Empty;
        public bool IsEnded { get; set; }
    }
}
