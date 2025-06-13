namespace SwimmingAcademy.DTOs
{
    public class AddSwimmerDto
    {
        public int UserID { get; set; }
        public short Site { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public short StartLevel { get; set; }
        public short Gender { get; set; }
        public short Club { get; set; }
        public string PrimaryPhone { get; set; }
        public string? SecondaryPhone { get; set; }
        public string PrimaryJop { get; set; }
        public string? SecondaryJop { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
