namespace SwimmingAcademy.DTOs
{
    public class UserLoginRequestDto
    {
        public int UserID { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
