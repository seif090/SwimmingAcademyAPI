namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Request DTO for deleting a swimmer.
    /// </summary>
    public class DeleteSwimmerRequest
    {
        /// <summary>
        /// The unique ID of the swimmer to be deleted.
        /// </summary>
        public long SwimmerID { get; set; }
    }
}
