namespace SwimmingAcademy.DTOs
{
    public class ChangeSwimmerSiteRequest
    {
        public long SwimmerID { get; set; }
        public int UserID { get; set; }
        public short Site { get; set; }
    }
}
