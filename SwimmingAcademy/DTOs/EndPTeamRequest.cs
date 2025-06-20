namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Request DTO for ending a Pre-Team session.
    /// </summary>
    public class EndPTeamRequest
    {
        /// <summary>
        /// The unique identifier of the Pre-Team to be ended.
        /// </summary>
        public long PTeamID { get; set; }

        /// <summary>
        /// The ID of the user performing the operation.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// The site (branch) where the Pre-Team is located.
        /// </summary>
        public short Site { get; set; }
    }
}
