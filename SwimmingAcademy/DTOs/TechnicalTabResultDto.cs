namespace SwimmingAcademy.DTOs
{
    public class TechnicalTabResultDto
    {
        public bool IsSchool { get; set; }
        public bool IsPreTeam { get; set; }
        public bool IsTeam { get; set; }

        public SwimmerTechnicalSchoolTabDto? SchoolData { get; set; }
        public SwimmerTechnicalPreTeamTabDto? PreTeamData { get; set; }
    }
}
