namespace SwimmingAcademy.DTOs
{
    public class UpdateSchoolRequest
    {
        public long SchoolID { get; set; }
        public int CoachID { get; set; }
        public string FirstDay { get; set; }
        public string SecondDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public short Type { get; set; }
        public short Site { get; set; }
        public int User { get; set; }
    }
}
