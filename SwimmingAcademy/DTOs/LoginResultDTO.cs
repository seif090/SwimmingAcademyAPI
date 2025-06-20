namespace SwimmingAcademy.DTOs
{
    public class LoginResultDTO
    {
        public string? Message { get; set; }
        public List<string> Sites { get; set; } = new();
        public List<string> Modules { get; set; } = new();
    }
}
