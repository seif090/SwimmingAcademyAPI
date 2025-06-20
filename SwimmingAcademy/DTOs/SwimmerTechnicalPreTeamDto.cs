namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Represents detailed technical data for a swimmer in the Pre-Team.
    /// </summary>
    public class SwimmerTechnicalPreTeamDto
    {
        /// <summary>
        /// The name of the coach.
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
        /// The third training day.
        /// </summary>
        public string ThirdDay { get; set; } = string.Empty;

        /// <summary>
        /// The start time of training.
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// The end time of training.
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// The swimmer’s current level.
        /// </summary>
        public string SwimmerLevel { get; set; } = string.Empty;

        /// <summary>
        /// Attendance status or frequency.
        /// </summary>
        public string Attendence { get; set; } = string.Empty;

        /// <summary>
        /// Last earned star level.
        /// </summary>
        public string LastStar { get; set; } = string.Empty;
    }
}
