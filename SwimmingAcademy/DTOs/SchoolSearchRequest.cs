namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Represents the filter criteria for searching school records.
    /// </summary>
    public class SchoolSearchRequest
    {
        /// <summary>
        /// The unique identifier of the school to search for.
        /// </summary>
        public long? SchoolID { get; set; }

        /// <summary>
        /// The full name of the coach or school.
        /// </summary>
        public string? FullName { get; set; } = string.Empty;

        /// <summary>
        /// The level of the school (e.g., beginner, intermediate).
        /// </summary>
        public short? Level { get; set; }

        /// <summary>
        /// The type of school (e.g., school vs. pre-team).
        /// </summary>
        public short? Type { get; set; }
    }
}
