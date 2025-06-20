namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Request DTO for creating a new school.
    /// </summary>
    public class CreateSchoolRequest
    {
        /// <summary>
        /// The level of the school (e.g. Beginner, Intermediate, etc.).
        /// </summary>
        public short SchoolLevel { get; set; }

        /// <summary>
        /// The ID of the assigned coach.
        /// </summary>
        public int CoachID { get; set; }

        /// <summary>
        /// The first training day of the week.
        /// </summary>
        public string? FirstDay { get; set; }

        /// <summary>
        /// The second training day of the week.
        /// </summary>
        public string? SecondDay { get; set; }

        /// <summary>
        /// The time when training starts.
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// The time when training ends.
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// The type of school (e.g. seasonal, monthly).
        /// </summary>
        public short Type { get; set; }

        /// <summary>
        /// The site (branch) where the school is held.
        /// </summary>
        public short Site { get; set; }

        /// <summary>
        /// The ID of the user creating the school.
        /// </summary>
        public int User { get; set; }
    }
}
