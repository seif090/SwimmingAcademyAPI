namespace SwimmingAcademy.DTOs
{
    public class UserLoginDetaisDto
    {
        public string FullName { get; set; }
        public short SiteSubId { get; set; }
        public string SiteDescription { get; set; }
        public short UserTypeSubId { get; set; }
        public string UserTypeDescription { get; set; }
        public List<UserActionDto> Actions { get; set; }
    }
}
