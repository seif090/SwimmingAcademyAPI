namespace SwimmingAcademy.DTOs
{
    public class PTeamSearchResultDto
    {
        public long PTeamID { get; set; }
        public string CoachName { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string Days { get; set; } = string.Empty;
        public string FromTo { get; set; } = string.Empty;
    }
}
