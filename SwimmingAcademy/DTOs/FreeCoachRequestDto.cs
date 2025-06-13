namespace SwimmingAcademy.DTOs
{
    public class FreeCoachRequestDto
    {
        public short Type { get; set; }           // 8 or 9
        public TimeSpan StartTime { get; set; }   // Ex: 14:00:00
        public string FirstDay { get; set; } = ""; // Ex: "Sunday"
        public short Site { get; set; }
    }
}
