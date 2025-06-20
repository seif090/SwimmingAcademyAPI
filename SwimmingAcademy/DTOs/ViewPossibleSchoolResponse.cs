namespace SwimmingAcademy.DTOs
{
    public class ViewPossibleSchoolResponse
    {
        public List<PossibleSchoolDto> Schools { get; set; } = new List<PossibleSchoolDto>();
        public List<InvoiceSuggestionDto> Invoices { get; set; } = new List<InvoiceSuggestionDto>();
    }
}
