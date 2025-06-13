namespace SwimmingAcademy.DTOs
{
    public class InvoiceItemDto
    {
        public string ItemName { get; set; }
        public short DurationInMonths { get; set; }
        public decimal Amount { get; set; }
    }
}
