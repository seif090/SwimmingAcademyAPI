namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Represents a swimmer's basic profile used in search results.
    /// </summary>
    public class SwimmerSearchResultDto
    {
        /// <summary>
        /// The full name of the swimmer.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// The registration or joining year of the swimmer.
        /// </summary>
        public string Year { get; set; } = string.Empty;

        /// <summary>
        /// The current level of the swimmer.
        /// </summary>
        public string CurrentLevel { get; set; } = string.Empty;

        /// <summary>
        /// The name of the coach responsible for the swimmer.
        /// </summary>
        public string CoachName { get; set; } = string.Empty;

        /// <summary>
        /// The site or branch the swimmer is associated with.
        /// </summary>
        public string Site { get; set; } = string.Empty;

        /// <summary>
        /// The club the swimmer is affiliated with.
        /// </summary>
        public string Club { get; set; } = string.Empty;
    }
}
