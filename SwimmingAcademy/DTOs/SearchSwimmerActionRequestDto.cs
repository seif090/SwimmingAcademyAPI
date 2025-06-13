namespace SwimmingAcademy.DTOs
{
    public class SearchSwimmerActionRequestDto
    {
        public int UserID { get; set; }
        public long SwimmerID { get; set; }
        public short UserSite { get; set; }
    }
}
