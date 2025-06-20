namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Request DTO for creating a new Pre-Team training group.
    /// </summary>
    public class CreatePTeamRequest
    {
        /// <summary>
        /// The level of the Pre-Team.
        /// </summary>
        public short PreTeamLevel { get; set; }

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
        /// The third training day of the week.
        /// </summary>
        public string? ThirdDay { get; set; }

        /// <summary>
        /// The training start time.
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// The training end time.
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// The site (branch) where the Pre-Team is located.
        /// </summary>
        public short Site { get; set; }

        /// <summary>
        /// The user ID of the person creating the Pre-Team.
        /// </summary>
        public int User { get; set; }
    }
}
