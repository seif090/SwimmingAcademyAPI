namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Request DTO for adding a new swimmer.
    /// </summary>
    public class AddSwimmerRequestDTO
    {
        /// <summary>
        /// ID of the user performing the action.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Site identifier where the swimmer is registered.
        /// </summary>
        public short Site { get; set; }

        /// <summary>
        /// Full name of the swimmer.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Date of birth of the swimmer.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Starting level of the swimmer.
        /// </summary>
        public short StartLevel { get; set; }

        /// <summary>
        /// Gender of the swimmer. (e.g., 0 = Male, 1 = Female)
        /// </summary>
        public short Gender { get; set; }

        /// <summary>
        /// Club identifier the swimmer belongs to.
        /// </summary>
        public short Club { get; set; }

        /// <summary>
        /// Primary contact phone number.
        /// </summary>
        public string PrimaryPhone { get; set; } = string.Empty;

        /// <summary>
        /// Secondary contact phone number (optional).
        /// </summary>
        public string? SecondaryPhone { get; set; }

        /// <summary>
        /// Primary job or occupation of the swimmer.
        /// </summary>
        public string PrimaryJob { get; set; } = string.Empty;

        /// <summary>
        /// Secondary job or occupation (optional).
        /// </summary>
        public string? SecondaryJob { get; set; }

        /// <summary>
        /// Email address of the swimmer (optional).
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Physical address of the swimmer.
        /// </summary>
        public string Address { get; set; } = string.Empty;
    }
}
