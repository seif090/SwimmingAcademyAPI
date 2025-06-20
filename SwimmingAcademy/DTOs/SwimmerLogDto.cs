namespace SwimmingAcademy.DTOs
{
    public class SwimmerLogDto
    {
        public required string ActionName { get; set; }
        public required string PerformedBy { get; set; }
        public required DateTime CreatedAtDate { get; set; }
        public required string Site { get; set; }
    }
}
