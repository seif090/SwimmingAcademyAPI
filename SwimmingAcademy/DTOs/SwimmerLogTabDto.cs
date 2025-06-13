namespace SwimmingAcademy.DTOs
{
    public class SwimmerLogTabDto
    {
        public string ActionName { get; set; } = string.Empty;
        public string PerformedBy { get; set; } = string.Empty;
        public DateTime CreatedAtDate { get; set; }
        public string Site { get; set; } = string.Empty;
    }
}
