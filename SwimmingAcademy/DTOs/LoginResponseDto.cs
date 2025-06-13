namespace SwimmingAcademy.DTOs
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public List<string> BranchNames { get; set; } = new();
        public string Token { get; set; } = string.Empty;
    }
}
