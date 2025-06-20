namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Represents the filter criteria used to search for free coaches.
    /// </summary>
    public class FreeCoachFilterRequest
    {
        /// <summary>
        /// The type of coaching (e.g., School = 1, PreTeam = 2).
        /// </summary>
        public short Type { get; set; }

        /// <summary>
        /// The starting time of the session to check coach availability.
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// The first day of the session (e.g., "Sunday", "Monday").
        /// </summary>
        public string? FirstDay { get; set; }

        /// <summary>
        /// The site (branch) where availability is being checked.
        /// </summary>
        public short Site { get; set; }
    }
}
