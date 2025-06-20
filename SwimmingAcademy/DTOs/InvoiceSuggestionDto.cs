namespace SwimmingAcademy.DTOs
{
    /// <summary>
    /// Represents a suggested invoice item for a swimmer's enrollment.
    /// </summary>
    public class InvoiceSuggestionDto
    {
        /// <summary>
        /// The name of the invoice item (e.g., School Program, PreTeam Program).
        /// </summary>
        public string ItemName { get; set; } = string.Empty;

        /// <summary>
        /// The duration of the item in months.
        /// </summary>
        public short DurationInMonths { get; set; }

        /// <summary>
        /// The total cost of the item.
        /// </summary>
        public decimal Amount { get; set; }
    }
}
