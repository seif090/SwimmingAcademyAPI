namespace SwimmingAcademy.DTOs
{
    public class UserLoginResponseDto
    {
        public string Message { get; set; } = string.Empty;
        public List<string> Sites { get; set; } = new();
        public List<string> Modules { get; set; } = new();
    }

}
