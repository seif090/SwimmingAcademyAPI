namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Represents a search filter for querying PreTeam records.
    /// </summary>
    public class PTeamSearchRequest
    {
        /// <summary>
        /// The ID of the PreTeam to filter by.
        /// </summary>
        public long PTeamID { get; set; }

        /// <summary>
        /// The swimmer's full name to filter by.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// The level to filter by.
        /// </summary>
        public short Level { get; set; }
    }
}
