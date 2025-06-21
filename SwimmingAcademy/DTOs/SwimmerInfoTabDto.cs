namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Represents detailed information about a swimmer for the Info tab.
    /// </summary>
    public class SwimmerInfoTabDto
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Site { get; set; } = string.Empty;
        public string CurrentLevel { get; set; } = string.Empty;
        public string StartLevel { get; set; } = string.Empty;
        public DateTime CreatedAtDate { get; set; }
        public string PrimaryJop { get; set; } = string.Empty;
        public string SecondaryJop { get; set; } = string.Empty;
        public string PrimaryPhone { get; set; } = string.Empty;
        public string SecondaryPhone { get; set; } = string.Empty;
        public string Club { get; set; } = string.Empty;
    }
}
