namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Represents detailed information about a swimmer for the Info tab.
    /// </summary>
    public class SwimmerInfoTabDto
    {
        /// <summary>
        /// Full name of the swimmer.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Birth date of the swimmer.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Site or branch where the swimmer is registered.
        /// </summary>
        public string Site { get; set; } = string.Empty;

        /// <summary>
        /// The swimmer's current level.
        /// </summary>
        public string CurrentLevel { get; set; } = string.Empty;

        /// <summary>
        /// The level at which the swimmer started.
        /// </summary>
        public string StartLevel { get; set; } = string.Empty;

        /// <summary>
        /// The date when the swimmer was created in the system.
        /// </summary>
        public DateTime CreatedAtDate { get; set; }

        /// <summary>
        /// The swimmer's primary job.
        /// </summary>
        public string PrimaryJop { get; set; } = string.Empty;

        /// <summary>
        /// The swimmer's secondary job (if any).
        /// </summary>
        public string SecondaryJop { get; set; } = string.Empty;

        /// <summary>
        /// Primary phone number of the swimmer.
        /// </summary>
        public string PrimaryPhone { get; set; } = string.Empty;

        /// <summary>
        /// Secondary phone number of the swimmer.
        /// </summary>
        public string SecondaryPhone { get; set; } = string.Empty;

        /// <summary>
        /// Name of the club the swimmer belongs to.
        /// </summary>
        public string Club { get; set; } = string.Empty;
    }
}
