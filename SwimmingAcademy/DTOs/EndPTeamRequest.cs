namespace SwimmingAcademy.DTOs
{
    public class EndPTeamRequest
    {
        public long PTeamID { get; set; }
        public int UserID { get; set; }
        public short Site
        {
            get; set;
        }
    }
}
