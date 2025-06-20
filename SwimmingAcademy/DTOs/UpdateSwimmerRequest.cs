namespace SwimmingAcademy.DTOs
{
    public class UpdateSwimmerRequest
    {
        public long SwimmerID { get; set; }
        public int UserID { get; set; }
        public short Site { get; set; }
        public required string FullName { get; set; } // Added 'required' modifier to fix CS8618
        public DateTime BirthDate { get; set; }
        public short Gender { get; set; }
        public short Club { get; set; }
        public required string PrimaryPhone { get; set; } // Added 'required' modifier to fix CS8618
        public string? SecondaryPhone { get; set; }
        public  string PrimaryJop { get; set; } = string.Empty; // Added 'required' modifier to fix CS8618
        public string? SecondaryJop { get; set; }
        public required string Email { get; set; } // Added 'required' modifier to fix CS8618
        public required string Adress { get; set; } // Added 'required' modifier to fix CS8618
    }
}
