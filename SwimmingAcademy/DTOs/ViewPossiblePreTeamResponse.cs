namespace SwimmingAcademy.DTOs
{
    public class ViewPossiblePreTeamResponse
    {
        public List<PossiblePreTeamDto> PreTeams { get; set; } = new List<PossiblePreTeamDto>();
        public List<InvoiceSuggestionDto> Invoices { get; set; } = new List<InvoiceSuggestionDto>();
    }
}
