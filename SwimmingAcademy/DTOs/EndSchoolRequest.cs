namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Request DTO for ending a school session.
    /// </summary>
    public class EndSchoolRequest
    {
        public long SchoolID { get; set; }
        public int UserID { get; set; }
        public short Site { get; set; }
    }
}
