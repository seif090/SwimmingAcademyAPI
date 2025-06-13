namespace SwimmingAcademy.Services.Interfaces
{
    public interface ICoachService
    {
        Task<IEnumerable<string>> GetFreeCoachesAsync(short type, TimeSpan startTime, string firstDay, short site);
    }
}
