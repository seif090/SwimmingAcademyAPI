namespace SwimmingAcademy.DTOs
{
    public class UpdateSchoolRequest
    {
        public long SchoolID { get; set; }
        public int CoachID { get; set; }
        public string FirstDay { get; set; } = string.Empty; // Fix: Initialize with a default value
        public string SecondDay { get; set; } = string.Empty; // Fix: Initialize with a default value
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public short Type { get; set; }
        public short Site { get; set; }
        public int User { get; set; }
    }
}
