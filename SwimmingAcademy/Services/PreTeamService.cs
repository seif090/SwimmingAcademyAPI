using SwimmingAcademy.Data;
using SwimmingAcademy.Models;
using Microsoft.EntityFrameworkCore;
using SwimmingAcademy.Services.Interfaces;
using SwimmingAcademy.DTOs;

namespace SwimmingAcademy.Services
{
    public class PreTeamService : IPreTeamService
    {
        private readonly SwimmingAcademyContext _context;

        public PreTeamService(SwimmingAcademyContext context)
        {
            _context = context;
        }

        public async Task<long> CreatePTeamAsync(
            short pTeamLevel,
            int coachId,
            string firstDay,
            string secondDay,
            string thirdDay,
            short site,
            int user,
            TimeSpan startTime,
            TimeSpan endTime)
        {
            var now = DateTime.Now;

            var preTeamInfo = new Info
            {
                PTeamLevel = pTeamLevel,
                CoachID = coachId,
                FirstDay = firstDay,
                SecondDay = secondDay,
                ThirdDay = thirdDay,
                site = site,
                createdAtSite = site,
                createdBy = user,
                createdAtDate = now.Date, // Use DateTime.Date to get the date part
                StartTime = (decimal)startTime.TotalHours, // Convert TimeSpan to decimal
                EndTime = (decimal)endTime.TotalHours     // Convert TimeSpan to decimal
            };

            _context.Infos.Add(preTeamInfo);
            await _context.SaveChangesAsync();

            // After SaveChanges, preTeamInfo.PteamID will be populated
            var preTeamLog = new log
            {
                PteamID = preTeamInfo.PTeamID,
                ActionID = 21,
                CreatedAtSite = site,
                createdby = user,
                createdAtDate = DateOnly.FromDateTime(now), // Use DateTime.Date to get the date part
            };

            _context.logs.Add(preTeamLog);
            await _context.SaveChangesAsync();

            return preTeamInfo.PTeamID;
        }

        public async Task EndPreTeamAsync(long pteamId, int userId, short site)
        {
            var now = DateTime.Now;

            // 1. Insert into PreTeam.Ended
            var ended = new Ended
            {
                PTeamID = pteamId,
                EndedAt = DateOnly.FromDateTime(now), // Use DateOnly.FromDateTime to convert DateTime to DateOnly
                EndedBy = userId
            };
            _context.Endeds.Add(ended);

            // 2. Delete from PreTeam.Details
            var details = _context.Details.Where(d => d.PTeamID == pteamId);
            _context.Details.RemoveRange(details);

            // 3. Insert into PreTeam.Log
            var log = new log
            {
                PteamID = pteamId,
                ActionID = 25,
                CreatedAtSite = site,
                createdby = userId,
                createdAtDate = DateOnly.FromDateTime(now) // Fixed conversion from DateTime to DateOnly
            };
            _context.logs.Add(log);

            await _context.SaveChangesAsync();
        }
        public async Task<PreTeamDetailsDto?> GetPTeamDetailsAsync(long pteamId)
        {
            var result = await (from i in _context.Infos
                                join c in _context.Coaches on i.CoachID equals c.CoachID
                                where i.PTeamID == pteamId
                                select new PreTeamDetailsDto
                                {
                                    FullName = c.FullName,
                                    FirstDay = i.FirstDay,
                                    SecondDay = i.SecondDay,
                                    ThirdDay = i.ThirdDay,
                                    StartTime = TimeSpan.FromHours((double)i.StartTime), // Convert decimal to TimeSpan
                                    EndTime = TimeSpan.FromHours((double)i.EndTime),   // Convert decimal to TimeSpan
                                    ISEnded = i.ISEnded
                                }).FirstOrDefaultAsync();

            return result;
        }
        public async Task<List<ActionNameDto>> SearchActionsAsync(int userId, long pteamId, short userSite)
        {
            // 1. Get userTypeID
            var userTypeId = await _context.users
                .Where(u => u.userid == userId)
                .Select(u => u.UserTypeID)
                .FirstOrDefaultAsync();

            // 2. Get PTeamSite
            var pteamSite = await _context.Infos
                .Where(i => i.PTeamID == pteamId)
                .Select(i => i.site)
                .FirstOrDefaultAsync();

            // 3. Build query based on site comparison
            bool sameSite = userSite == pteamSite;

            var actionsQuery = from up in _context.Users_Privs
                               join a in _context.Actions on up.ActionID equals a.ActionID
                               where up.UserTypeID == userTypeId
                                     && a.Module == "Mod_PT"
                                     && a.SameSite == sameSite // Fix: Compare bool? with bool directly
                                     && a.ActionID != 21
                               select new ActionNameDto
                               {
                                   ActionName = a.ActionName
                               };

            var actions = await actionsQuery
                .OrderBy(a => a.ActionName)
                .ToListAsync();

            return actions;
        }
        public async Task<List<ShowPreTeamDto>> ShowPreTeamAsync(long? pteamId, string? fullName, short? level)
        {
            var query = from i in _context.Infos
                        join c in _context.Coaches on i.CoachID equals c.CoachID
                        join l in _context.AppCodes on i.PTeamLevel equals l.sub_id
                        select new ShowPreTeamDto
                        {
                            CoachName = c.FullName,
                            Level = l.description,
                            Days = i.FirstDay + " - " + i.SecondDay + " - " + i.ThirdDay,
                            FromTo = i.StartTime.ToString(@"hh\:mm") + " : " + i.EndTime.ToString(@"hh\:mm")
                        };

            if (pteamId.HasValue)
            {
                query = query.Where(x => x.CoachName == pteamId.Value.ToString()); // Fix: Replace 'i' with 'x' to match the query projection
            }
            else if (!string.IsNullOrEmpty(fullName))
            {
                query = query.Where(x => x.CoachName.Contains(fullName)); // Fix: Replace 'i' with 'x' to match the query projection
            }
            else if (level.HasValue)
            {
                query = query.Where(x => x.Level == level.Value.ToString()); // Fix: Replace 'i' with 'x' to match the query projection
            }
            else
            {
                return new List<ShowPreTeamDto>();
            }

            return await query.ToListAsync();
        }
        public async Task<List<SwimmerDetailsTabDto>> GetSwimmerDetailsTabAsync(long pteamId)
        {
            var result = await (from d in _context.Details
                                join i in _context.Infos1 on d.SwimmerID equals i.SwimmerID
                                join level in _context.AppCodes on i.CurrentLevel equals level.sub_id
                                join star in _context.AppCodes on d.LastStar equals star.sub_id
                                where d.PTeamID == pteamId
                                select new SwimmerDetailsTabDto
                                {
                                    FullName = i.FulllName,
                                    Attendence = d.Attendence,
                                    SwimmerLevel = level.description,
                                    LastStar = star.description
                                }).ToListAsync();

            return result;
        }
        public async Task UpdatePTeamAsync(UpdatePreTeamDto dto)
        {
            var pteam = await _context.Infos.FirstOrDefaultAsync(i => i.PTeamID == dto.PTeamID);
            if (pteam == null)
                throw new InvalidOperationException("PreTeam not found.");

            pteam.CoachID = dto.CoachID;
            pteam.FirstDay = dto.FirstDay;
            pteam.SecondDay = dto.SecondDay;
            pteam.ThirdDay = dto.ThirdDay;
            pteam.updatedAtSite = dto.Site;
            pteam.updatedBy = dto.User;
            pteam.updatedAtDate = DateTime.Now;

            // Convert TimeSpan to decimal for StartTime and EndTime
            pteam.StartTime = (decimal)dto.StartTime.TotalHours;
            pteam.EndTime = (decimal)dto.EndTime.TotalHours;

            // Log the update action
            var log = new log
            {
                PteamID = dto.PTeamID,
                ActionID = 23,
                CreatedAtSite = dto.Site,
                createdby = dto.User,
                createdAtDate = DateOnly.FromDateTime(DateTime.Now) // Fix: Convert DateTime to DateOnly
            };
            _context.logs.Add(log);

            await _context.SaveChangesAsync();
        }
    }
}
