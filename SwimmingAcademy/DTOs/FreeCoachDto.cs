namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Represents a coach who is currently available (not assigned to a school or team).
    /// </summary>
    public class FreeCoachDto
    {
        /// <summary>
        /// The full name of the free coach.
        /// </summary>
        public string? Name { get; set; }
    }
}
