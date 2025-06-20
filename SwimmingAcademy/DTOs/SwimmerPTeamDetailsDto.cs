namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Represents detailed information about a swimmer in a Pre-Team.
    /// </summary>
    public class SwimmerPTeamDetailsDto
    {
        /// <summary>
        /// The full name of the swimmer.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// The attendance record of the swimmer.
        /// </summary>
        public string Attendance { get; set; } = string.Empty;

        /// <summary>
        /// The current level of the swimmer.
        /// </summary>
        public string SwimmerLevel { get; set; } = string.Empty;

        /// <summary>
        /// The last star or performance badge the swimmer received.
        /// </summary>
        public string LastStar { get; set; } = string.Empty;
    }
}
