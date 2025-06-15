namespace SwimmingAcademy.DTOs
{
    public class ViewPossibleSchoolResponse
    {
        public List<PossibleSchoolDto> Schools { get; set; }
        public List<InvoiceSuggestionDto> Invoices { get; set; }
    }
}
