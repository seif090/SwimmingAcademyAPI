namespace SwimmingAcademy.DTOs
{
    public class UpdateSwimmerResponseDto
    {
        public long SwimmerID { get; set; }
        public string Message { get; set; } = "Swimmer updated successfully.";
    }
}
