namespace SwimmingAcademy.DTOs
{
    public class SearchSchoolActionRequestDto
    {
        public int UserID { get; set; }
        public long SchoolID { get; set; }
        public short UserSite { get; set; }
    }
}
