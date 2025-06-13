namespace SwimmingAcademy.DTOs
{
    public class UpdateSwimmerLevelResponseDto
    {
        public long SwimmerID { get; set; }
        public short NewLevel { get; set; }
        public string Message { get; set; } = "Swimmer level updated successfully.";
    }
}
