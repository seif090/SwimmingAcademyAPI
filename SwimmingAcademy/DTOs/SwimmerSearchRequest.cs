namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Represents a request to search for swimmers using one of several filters.
    /// </summary>
    public class SwimmerSearchRequest
    {
        /// <summary>
        /// The ID of the swimmer to search for. Optional.
        /// </summary>
        public long? SwimmerID { get; set; }

        /// <summary>
        /// The full name of the swimmer. Optional.
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// The registration year or year-related filter. Optional.
        /// </summary>
        public string? Year { get; set; }

        /// <summary>
        /// The level of the swimmer. Optional.
        /// </summary>
        public short? Level { get; set; }
    }
}
