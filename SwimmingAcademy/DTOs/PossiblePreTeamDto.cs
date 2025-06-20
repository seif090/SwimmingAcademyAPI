namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Represents a suggested PreTeam training option for a swimmer.
    /// </summary>
    public class PossiblePreTeamDto
    {
        /// <summary>
        /// The name of the coach handling the PreTeam.
        /// </summary>
        public string CoachName { get; set; } = string.Empty;

        /// <summary>
        /// The training days assigned to the PreTeam.
        /// </summary>
        public string Dayes { get; set; } = string.Empty;

        /// <summary>
        /// The start and end times for the PreTeam training.
        /// </summary>
        public string FromTo { get; set; } = string.Empty;
    }
}
