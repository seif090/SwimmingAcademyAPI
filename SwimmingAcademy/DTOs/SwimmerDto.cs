namespace SwimmingAcademy.DTOs
{
    public class SwimmerDto
    {
        public long SwimmerID { get; set; }
        public string FulllName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int CurrentLevel { get; set; }
        public int Site { get; set; }
        public int Gender { get; set; }
        public int Club { get; set; }
    }
}
