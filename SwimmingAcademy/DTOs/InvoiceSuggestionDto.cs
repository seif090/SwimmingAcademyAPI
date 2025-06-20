namespace SwimmingAcademy.DTOs
{
    public class InvoiceSuggestionDto
    {
        public string? ItemName { get; set; }
        public short DurationInMonths { get; set; }
        public decimal Amount { get; set; }
    }
}
