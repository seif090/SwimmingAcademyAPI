using Microsoft.EntityFrameworkCore;
using SwimmingAcademy.Data;
using SwimmingAcademy.Models;
using SwimmingAcademy.Repositories.Interfaces;

namespace SwimmingAcademy.Repositories
{
    public class SwimmerRepository : ISwimmerRepository
    {
        private readonly SwimmingAcademyContext _context;

        public SwimmerRepository(SwimmingAcademyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Info2>> GetAllAsync() =>
            await _context.Infos1.ToListAsync();

        public async Task<Info2?> GetByIdAsync(long id) =>
            await _context.Infos1.FindAsync(id);

        public async Task AddAsync(Info2 swimmer) =>
            await _context.Infos1.AddAsync(swimmer);

        public void Update(Info2 swimmer) =>
            _context.Infos1.Update(swimmer);

        public void Delete(Info2 swimmer) =>
            _context.Infos1.Remove(swimmer);

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();
    }
}

