namespace SwimmingAcademy.DTOs
{
    public class SchoolDetailsTabDto
    {
        public string FullName { get; set; }
        public string FirstDay { get; set; }
        public string SecondDay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int Capacity { get; set; }
        public string NumberOfSwimmers { get; set; }
        public bool IsEnded { get; set; }
    }
}
