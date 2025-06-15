namespace SwimmingAcademy.DTOs
{
    public class UpdateSwimmerLevelRequest
    {
        public long SwimmerID { get; set; }
        public short NewLevel { get; set; }
        public int UserID { get; set; }
        public short Site { get; set; }
    }
}
