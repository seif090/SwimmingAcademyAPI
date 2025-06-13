namespace SwimmingAcademy.DTOs
{
    public class ViewPossibleSchoolResponseDto
    {
        public List<SchoolOptionsDto> Schools { get; set; } = new();
        public List<InvoiceItemDto> Invoices { get; set; } = new();
    }
}
