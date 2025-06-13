namespace SwimmingAcademy.DTOs
{
    public class CreateSchoolDto
    {
        public short SchoolLevel { get; set; }
        public int CoachID { get; set; }
        public string FirstDay { get; set; } = string.Empty;
        public string SecondDay { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public short Type { get; set; }        // School Type
        public short Site { get; set; }
        public int User { get; set; }
    }
}
