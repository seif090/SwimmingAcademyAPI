namespace SwimmingAcademy.DTOs
{
    public class PTeamSearchResultDto
    {
        public long PTeamID { get; set; }
        public string CoachName { get; set; } = string.Empty; // Fix: Initialize with a default value
        public string Level { get; set; } = string.Empty; // Fix: Initialize with a default value
        public string Days { get; set; } = string.Empty; // Fix: Initialize with a default value
        public string FromTo { get; set; } = string.Empty; // Fix: Initialize with a default value
    }
}
