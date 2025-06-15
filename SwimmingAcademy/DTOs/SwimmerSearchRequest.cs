namespace SwimmingAcademy.DTOs
{
    public class SwimmerSearchRequest
    {
        public long? SwimmerID { get; set; }
        public string? FullName { get; set; }
        public string? Year { get; set; }
        public short? Level { get; set; }
    }
}
