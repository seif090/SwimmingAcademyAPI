namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Request DTO for ending a school session.
    /// </summary>
    public class EndSchoolRequest
    {
        /// <summary>
        /// The unique identifier of the school to be ended.
        /// </summary>
        public long SchoolID { get; set; }

        /// <summary>
        /// The ID of the user performing the action.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// The site (branch) where the school is located.
        /// </summary>
        public short Site { get; set; }
    }
}
