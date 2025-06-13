using SwimmingAcademy.Models;

namespace SwimmingAcademy.Repositories.Interfaces
{
    public interface ISwimmerRepository
    {
        Task<IEnumerable<Info2>> GetAllAsync();
        Task<Info2?> GetByIdAsync(long id);
        Task AddAsync(Info2 swimmer);
        void Update(Info2 swimmer);
        void Delete(Info2 swimmer);
        Task SaveAsync();
    }
}
