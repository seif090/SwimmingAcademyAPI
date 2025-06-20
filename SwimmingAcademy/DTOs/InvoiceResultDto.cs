namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Represents the result of an invoice transaction for a swimmer.
    /// </summary>
    public class InvoiceResultDto
    {
        /// <summary>
        /// The name of the invoiced item (e.g., School Fee, PreTeam Fee).
        /// </summary>
        public string InvItem { get; set; } = string.Empty;

        /// <summary>
        /// The monetary value of the invoiced item.
        /// </summary>
        public decimal Value { get; set; }
    }
}
