namespace SwimmingAcademy.DTOs
{
    public class FreeCoachFilterRequest
    {
        public short Type { get; set; }
        public TimeSpan StartTime { get; set; }
        public string? FirstDay { get; set; }
        public short Site { get; set; }
    }
}
