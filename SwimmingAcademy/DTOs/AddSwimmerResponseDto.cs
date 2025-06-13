namespace SwimmingAcademy.DTOs
{
    public class AddSwimmerResponseDto
    {
        public long SwimmerID { get; set; }
        public string Message { get; set; } = "Swimmer added successfully.";
    }
}
