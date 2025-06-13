using AutoMapper;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Repositories.Interfaces;
using SwimmingAcademy.Services.Interfaces;
using SwimmingAcademy.Data;
using SwimmingAcademy.Models;
using Microsoft.EntityFrameworkCore;

namespace SwimmingAcademy.Services
{
    public class SwimmerService : ISwimmerService
    {
        private readonly ISwimmerRepository _repository;
        private readonly IMapper _mapper;
        private readonly SwimmingAcademyContext _context;

        public SwimmerService(ISwimmerRepository repository, IMapper mapper, SwimmingAcademyContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<SwimmerDto>> GetAllSwimmersAsync()
        {
            var swimmers = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<SwimmerDto>>(swimmers);
        }

        public async Task<SwimmerDto?> GetSwimmerById(long id)
        {
            var swimmer = await _repository.GetByIdAsync(id);
            return swimmer == null ? null : _mapper.Map<SwimmerDto>(swimmer);
        }

        public async Task<long> AddSwimmerAsync(AddSwimmerDto dto)
        {
            var now = DateTime.Now;

            var swimmerInfo = new Info2
            {
                FulllName = dto.FullName,
                BirthDate = DateOnly.FromDateTime(dto.BirthDate), // Fix for CS0029: Convert DateTime to DateOnly
                StartLevel = dto.StartLevel,
                CurrentLevel = dto.StartLevel,
                Site = dto.Site,
                Gender = dto.Gender,
                CLub = dto.Club,
                CreatedAtSite = dto.Site,
                CreatedBy = dto.UserID,
                createdAtDate = now
            };
            _context.Infos1.Add(swimmerInfo); // Fix for CS1503: Correct DbSet reference to Infos1
            await _context.SaveChangesAsync();

            // Insert into Swimmers.Parent
            var swimmerParent = new Parent
            {
                SwimmerID = swimmerInfo.SwimmerID,
                SwimmerName = dto.FullName,
                PrimaryPhone = dto.PrimaryPhone,
                SecondaryPhone = dto.SecondaryPhone,
                PrimaryJop = dto.PrimaryJop,
                SecondaryJop = dto.SecondaryJop,
                Email = dto.Email,
                Address = dto.Address
            };
            _context.Parents.Add(swimmerParent);
            var swimmerLog = new log2
            {
                swimmerID = swimmerInfo.SwimmerID,
                ActionID = 1,
                createdAtsite = dto.Site,
                CreatedBy = dto.UserID,
                CreatedAtDate = DateOnly.FromDateTime(now) // Fix for CS0029: Convert DateTime to DateOnly
            };
            _context.logs2.Add(swimmerLog);

            await _context.SaveChangesAsync();

            return swimmerInfo.SwimmerID;
        }
        public async Task<long> ChangeSiteAsync(ChangeSiteDto dto)
        {
            // 1. Get the user's site
            var site = await _context.users
                .Where(u => u.userid == dto.UserID)
                .Select(u => u.Site)
                .FirstOrDefaultAsync();

            if (site == default)
                throw new InvalidOperationException("User site not found.");

            // 2. Update Swimmers.Info
            var swimmerInfo = await _context.Infos1
                .FirstOrDefaultAsync(s => s.SwimmerID == dto.SwimmerID);
            if (swimmerInfo != null)
                swimmerInfo.Site = site;

            // 3. Update Swimmers.Technical
            var swimmerTechnical = await _context.Technicals
                .Where(t => t.SwimmerID == dto.SwimmerID)
                .ToListAsync();
            foreach (var tech in swimmerTechnical)
                tech.Site = site;

            // 4. Insert into Swimmers.log
            var log = new log2
            {
                swimmerID = dto.SwimmerID,
                ActionID = 16,
                createdAtsite = site,
                CreatedBy = dto.UserID,
                CreatedAtDate = DateOnly.FromDateTime(DateTime.Now) // Fix for CS0029: Convert DateTime to DateOnly
            };
            _context.logs2.Add(log);

            await _context.SaveChangesAsync();

            return dto.SwimmerID;
        }
        public async Task DropSwimmerAsync(long swimmerId)
        {
            // Swimmers.Info
            var swimmerInfo = await _context.Infos1.FirstOrDefaultAsync(s => s.SwimmerID == swimmerId);
            if (swimmerInfo != null)
                _context.Infos1.Remove(swimmerInfo);

            // Swimmers.Parent
            var parents = _context.Parents.Where(p => p.SwimmerID == swimmerId);
            _context.Parents.RemoveRange(parents);

            // Swimmers.Technical
            var technicals = _context.Technicals.Where(t => t.SwimmerID == swimmerId);
            _context.Technicals.RemoveRange(technicals);

            // Swimmers.log
            var swimmerLogs = _context.logs2.Where(l => l.swimmerID == swimmerId);
            _context.logs2.RemoveRange(swimmerLogs);

            // Schools.Details
            var schoolDetails = _context.Details1.Where(d => d.SwimmerID == swimmerId);
            _context.Details1.RemoveRange(schoolDetails);

            // PreTeam.Details
            var preTeamDetails = _context.Details.Where(d => d.SwimmerID == swimmerId);
            _context.Details.RemoveRange(preTeamDetails);

            // Schools.log
            var schoolLogs = _context.logs1.Where(l => l.swimmerID == swimmerId);
            _context.logs1.RemoveRange(schoolLogs);

            // PreTeam.log
            var preTeamLogs = _context.logs.Where(l => l.SwimmerID == swimmerId);
            _context.logs.RemoveRange(preTeamLogs);

            await _context.SaveChangesAsync();
        }
    }
}
