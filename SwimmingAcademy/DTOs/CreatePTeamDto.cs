public class CreatePTeamDto
{
    public short PTeamLevel { get; set; }
    public int CoachID { get; set; }
    public string FirstDay { get; set; }
    public string SecondDay { get; set; }
    public string ThirdDay { get; set; }
    public short Site { get; set; }
    public int User { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}