namespace SwimmingAcademy.DTOs
{
    public class InvoiceItemDto
    {
        public string ItemName { get; set; } = string.Empty;
        public short DurationInMonths { get; set; }
        public decimal Amount { get; set; }
    }
}
