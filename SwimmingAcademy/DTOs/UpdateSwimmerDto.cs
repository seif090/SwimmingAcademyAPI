namespace SwimmingAcademy.DTOs
{
    public class UpdateSwimmerDto
    {
        public long SwimmerID { get; set; }
        public int UserID { get; set; }
        public short Site { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public short Gender { get; set; }
        public short Club { get; set; }
        public string PrimaryPhone { get; set; } = string.Empty;
        public string? SecondaryPhone { get; set; }
        public string PrimaryJop { get; set; } = string.Empty;
        public string? SecondaryJop { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
    }
}
