namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Represents the filter criteria used to search for free coaches.
    /// </summary>
    public class FreeCoachFilterRequest
    {
        public short Type { get; set; }
        public TimeSpan StartTime { get; set; }
        public string FirstDay { get; set; } = string.Empty;
        public short Site { get; set; }
    }
}
