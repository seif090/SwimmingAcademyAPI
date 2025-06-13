namespace SwimmingAcademy.DTOs
{
    public class ShowSchoolRequestDto
    {
        public long? SchoolID { get; set; }
        public string? FullName { get; set; }
        public short? Level { get; set; }
        public short? Type { get; set; }
    }
}
