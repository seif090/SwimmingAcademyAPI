namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Represents a single log entry of a swimmer's activity or changes.
    /// </summary>
    public class SwimmerLogDto
    {
        /// <summary>
        /// The name of the action performed (e.g., "Added to School").
        /// </summary>
        public string ActionName { get; set; } = string.Empty;

        /// <summary>
        /// The name of the user who performed the action.
        /// </summary>
        public string PerformedBy { get; set; } = string.Empty;

        /// <summary>
        /// The date and time when the action occurred.
        /// </summary>
        public DateTime CreatedAtDate { get; set; }

        /// <summary>
        /// The site or branch where the action took place.
        /// </summary>
        public string Site { get; set; } = string.Empty;
    }
}
