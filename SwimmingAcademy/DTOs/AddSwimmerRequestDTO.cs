namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Request DTO for adding a new swimmer.
    /// </summary>
    public class AddSwimmerRequestDTO
    {
        public int UserID { get; set; }
        public short Site { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public short StartLevel { get; set; }
        public short Gender { get; set; }
        public short Club { get; set; }
        public string PrimaryPhone { get; set; } = string.Empty;
        public string? SecondaryPhone { get; set; }
        public string PrimaryJob { get; set; } = string.Empty;
        public string? SecondaryJob { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
