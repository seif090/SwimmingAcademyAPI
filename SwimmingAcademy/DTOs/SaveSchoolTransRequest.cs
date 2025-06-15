namespace SwimmingAcademy.DTOs
{
    public class SaveSchoolTransRequest
    {
        public long SwimmerID { get; set; }
        public long SchoolID { get; set; }
        public int DuarationsInMonths { get; set; }
        public int User { get; set; }
        public short Site { get; set; }
    }
}
