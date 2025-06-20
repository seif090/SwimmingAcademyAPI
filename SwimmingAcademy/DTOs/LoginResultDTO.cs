namespace SwimmingAcademy.DTOs
{
    public class LoginResultDTO
    {
        public string Message { get; set; } = string.Empty;
        public List<string> Sites { get; set; } = new(); // ✅ Safe
        public List<string> Modules { get; set; } = new(); // ✅ Safe
    }

}
