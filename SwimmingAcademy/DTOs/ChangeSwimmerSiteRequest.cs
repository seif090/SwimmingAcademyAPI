namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Request DTO for changing a swimmer's site.
    /// </summary>
    public class ChangeSwimmerSiteRequest
    {
        public long SwimmerID { get; set; }
        public int UserID { get; set; }
        public short Site { get; set; }
    }
}
