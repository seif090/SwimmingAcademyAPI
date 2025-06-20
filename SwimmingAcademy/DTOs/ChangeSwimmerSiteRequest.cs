namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Request DTO for changing a swimmer's site.
    /// </summary>
    public class ChangeSwimmerSiteRequest
    {
        /// <summary>
        /// The ID of the swimmer whose site is to be changed.
        /// </summary>
        public long SwimmerID { get; set; }

        /// <summary>
        /// The ID of the user performing the site change.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// The new site identifier.
        /// </summary>
        public short Site { get; set; }
    }
}
