namespace SwimmingAcademy.DTOs
{
    public class PossibleSchoolDto
    {
        public string CoachName { get; set; } = string.Empty; // Initialize with a default value to avoid nullability issues
        public string Dayes { get; set; } = string.Empty; // Initialize with a default value to avoid nullability issues
        public string FromTo { get; set; } = string.Empty; // Initialize with a default value to avoid nullability issues
    }
}
