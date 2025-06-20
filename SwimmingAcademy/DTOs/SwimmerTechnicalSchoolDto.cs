namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Represents the technical training details for a swimmer enrolled in a school.
    /// </summary>
    public class SwimmerTechnicalSchoolDto
    {
        /// <summary>
        /// The coach's full name.
        /// </summary>
        public string CoachName { get; set; } = string.Empty;

        /// <summary>
        /// The first training day.
        /// </summary>
        public string FirstDay { get; set; } = string.Empty;

        /// <summary>
        /// The second training day.
        /// </summary>
        public string SecondDay { get; set; } = string.Empty;

        /// <summary>
        /// Training session start time.
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Training session end time.
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// The current level of the swimmer.
        /// </summary>
        public string SwimmerLevel { get; set; } = string.Empty;

        /// <summary>
        /// Attendance status or details.
        /// </summary>
        public string Attendence { get; set; } = string.Empty;
    }
}
