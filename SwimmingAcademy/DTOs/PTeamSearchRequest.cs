namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Represents a search filter for querying PreTeam records.
    /// </summary>
    public class PTeamSearchRequest
    {
        public long? PTeamID { get; set; }
        public string? FullName { get; set; }
        public short? Level { get; set; }
    }
}
