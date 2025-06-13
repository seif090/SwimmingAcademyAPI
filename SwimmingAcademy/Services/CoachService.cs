using Microsoft.EntityFrameworkCore;
using SwimmingAcademy.Data;
using SwimmingAcademy.Models;
using SwimmingAcademy.Services.Interfaces;

namespace SwimmingAcademy.Services
{
    public class CoachService : ICoachService
    {
        private readonly SwimmingAcademyContext _context;

        public CoachService(SwimmingAcademyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> GetFreeCoachesAsync(short type, TimeSpan startTime, string firstDay, short site)
        {
            IQueryable<Coach> coachesQuery = _context.Coaches
                .Where(c => c.CoachType == type && c.site == site);

            // For type 8, join with SchoolsInfo; for type 9, join with PreTeamInfo
            if (type == 8)
            {
                var query = from c in coachesQuery
                            join i in _context.Infos
                                on c.CoachID equals i.CoachID into infoJoin
                            from i in infoJoin.DefaultIfEmpty()
                            select new
                            {
                                c.FullName,
                                c.CoachID,
                                StartTime = i != null ? (TimeSpan?)TimeSpan.FromHours((double)i.StartTime) : null,
                                Day = firstDay
                            };

                var freeCoaches = await query
                    .Where(x => !(x.StartTime == startTime && x.Day == firstDay))
                    .Select(x => x.FullName)
                    .Distinct()
                    .ToListAsync();

                return freeCoaches;
            }
            else if (type == 9)
            {
                var query = from c in coachesQuery
                            join i in _context.Infos
                                on c.CoachID equals i.CoachID into infoJoin
                            from i in infoJoin.DefaultIfEmpty()
                            select new
                            {
                                c.FullName,
                                c.CoachID,
                                StartTime = i != null ? (TimeSpan?)TimeSpan.FromHours((double)i.StartTime) : null,
                                Day = firstDay
                            };

                var freeCoaches = await query
                    .Where(x => !(x.StartTime == startTime && x.Day == firstDay))
                    .Select(x => x.FullName)
                    .Distinct()
                    .ToListAsync();

                return freeCoaches;
            }
            else
            {
                return Enumerable.Empty<string>();
            }
        }
    }
}
