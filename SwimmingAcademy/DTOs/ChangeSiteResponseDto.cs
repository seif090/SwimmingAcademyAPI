namespace SwimmingAcademy.DTOs
{
    public class ChangeSiteResponseDto
    {
        public long SwimmerID { get; set; }
        public string Message { get; set; } = "Swimmer site changed successfully.";
    }
}
