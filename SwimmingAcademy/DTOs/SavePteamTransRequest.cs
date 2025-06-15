namespace SwimmingAcademy.DTOs
{
    public class SavePteamTransRequest
    {
        public long SwimmerID { get; set; }
        public long PTeamID { get; set; }
        public int DuarationsInMonths { get; set; }
        public int User { get; set; }
        public short Site { get; set; }
    }
}
