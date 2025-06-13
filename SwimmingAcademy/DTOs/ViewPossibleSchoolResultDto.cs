namespace SwimmingAcademy.DTOs
{
    public class ViewPossibleSchoolResultDto
    {
        public List<PossibleSchoolDto> Schools { get; set; } = new();
        public List<InvoiceItemDto> Invoices { get; set; } = new();
    }
}
