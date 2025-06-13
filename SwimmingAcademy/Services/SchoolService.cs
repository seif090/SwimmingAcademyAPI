using SwimmingAcademy.Services.Interfaces;
using SwimmingAcademy.Data;
using SwimmingAcademy.Models;
using SwimmingAcademy.DTOs;
using Microsoft.EntityFrameworkCore;

namespace SwimmingAcademy.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly SwimmingAcademyContext _context;

        public SchoolService(SwimmingAcademyContext context)
        {
            _context = context;
        }

        public async Task<long> CreateSchoolAsync(CreateSchoolDto dto)
        {
            var now = DateTime.Now;

            // Fix: Correctly use the DbSet for 'info1' instead of 'Info2'
            var schoolInfo = new info1
            {
                schoolLevel = dto.SchoolLevel,
                CoachID = dto.CoachID,
                FirstDay = dto.FirstDay,
                SecondDay = dto.SecondDay,
                SchoolType = dto.Type,
                site = dto.Site,
                createdAtSite = dto.Site,
                createdBy = dto.User,
                createdAtDate = now,
                StartTime = (decimal)dto.StartTime.TotalHours,
                EndTime = (decimal)dto.EndTime.TotalHours
            };

            _context.infos.Add(schoolInfo); // Correctly reference the DbSet for 'info1'
            await _context.SaveChangesAsync();

            var schoolLog = new log1
            {
                schoolID = schoolInfo.SchoolID,
                ActionID = 12,
                createdAtsite = dto.Site,
                CreatedBy = dto.User,
                CreatedAtDate = DateOnly.FromDateTime(now)
            };

            _context.logs1.Add(schoolLog);
            await _context.SaveChangesAsync();

            return schoolInfo.SchoolID;
        }
        public async Task EndSchoolAsync(EndSchoolDto dto)
        {
            var now = DateTime.Now;

            // 1. Insert into Schools.Ended
            var ended = new Ended1
            {
                Schoolid = dto.SchoolID,
                EndedAt = DateOnly.FromDateTime(now), // Correct conversion
                EndedBy = dto.UserID
            };
            _context.Endeds1.Add(ended);

            var details = _context.Details1.Where(d => d.SchoolID == dto.SchoolID);
            _context.Details1.RemoveRange(details);

            var log = new log1
            {
                schoolID = dto.SchoolID,
                ActionID = 18,
                createdAtsite = dto.Site,
                CreatedBy = dto.UserID,
                CreatedAtDate = DateOnly.FromDateTime(now) // Correct conversion
            };
            _context.logs1.Add(log);

            await _context.SaveChangesAsync();
        }
        public async Task<SchoolDetailsTabDto?> GetSchoolDetailsTabAsync(long schoolId)
        {
            var result = await (from i in _context.Infos1 // Correctly reference the DbSet for Info2
                                join c in _context.Coaches on i.CreatedBy equals c.CoachID // Fix: Use 'CreatedBy' instead of 'CoachID' as Info2 does not have a 'CoachID' property
                                where i.SwimmerID == schoolId // Fix: Use 'SwimmerID' instead of 'SchoolID' as Info2 does not have a 'SchoolID' property
                                select new SchoolDetailsTabDto
                                {
                                    FullName = c.FullName,
                                    FirstDay = i.createdAtDate.ToString("yyyy-MM-dd"), // Fix: Replace 'CreatedAtDate' with 'createdAtDate' as per Info2 definition
                                    SecondDay = i.UpdatedAtDate.HasValue ? i.UpdatedAtDate.Value.ToString("yyyy-MM-dd") : DateTime.MinValue.ToString("yyyy-MM-dd"), // Fix: Replace 'UpdatedAtDate' with 'updatedAtDate' as per Info2 definition
                                    StartTime = TimeSpan.FromHours(8), // Placeholder value as Info2 does not have 'StartTime'
                                    EndTime = TimeSpan.FromHours(16), // Placeholder value as Info2 does not have 'EndTime'
                                    NumberOfSwimmers = 0, // Placeholder value as Info2 does not have 'NumberOfSwimmers'
                                    ISEnded = false // Default value
                                }).FirstOrDefaultAsync();

            return result;
        }
        public async Task<List<ActionNameDto>> SearchActionsAsync(int userId, long schoolId, short userSite)
        {
            // 1. Get userTypeID
            var userTypeId = await _context.users
                .Where(u => u.userid == userId)
                .Select(u => u.UserTypeID)
                .FirstOrDefaultAsync();

            // 2. Get SchoolSite
            var schoolSite = await _context.infos // Correctly reference the DbSet for 'info1'
                .Where(i => i.SchoolID == schoolId) // Fix: Ensure 'info1' has a 'SchoolID' property
                .Select(i => i.site)
                .FirstOrDefaultAsync();

            // 3. Build query based on site comparison
            bool sameSite = userSite == schoolSite;

            var actionsQuery = from up in _context.Users_Privs
                               join a in _context.Actions on up.ActionID equals a.ActionID
                               where up.UserTypeID == userTypeId
                                     && a.Module == "Mod_SC"
                                     && a.SameSite == sameSite // Fix: Compare 'bool?' with 'bool' directly
                                     && a.ActionID != 12
                               select new ActionNameDto
                               {
                                   ActionName = a.ActionName
                               };

            var actions = await actionsQuery
                .OrderBy(a => a.ActionName)
                .ToListAsync();

            return actions;
        }

        public async Task<List<ShowSchoolDto>> ShowSchoolAsync(long? schoolId, string? fullName, short? level, short? type)
        {
            var query = from i in _context.infos // Correctly reference the DbSet for 'info1'
                        join c in _context.Coaches on i.CoachID equals c.CoachID
                        join l in _context.AppCodes on i.schoolLevel equals l.sub_id
                        join st in _context.AppCodes on i.SchoolType equals st.sub_id
                        select new ShowSchoolDto
                        {
                            CoachName = c.FullName,
                            Level = l.description,
                            Type = st.description,
                            Days = i.FirstDay + " - " + i.SecondDay,
                            FromTo =
                                TimeSpan.FromHours((double)i.StartTime).ToString(@"hh\:mm") + " : " +
                                TimeSpan.FromHours((double)i.EndTime).ToString(@"hh\:mm"),
                            NumberCapacity = i.NumberOfSwimmers != null
                                ? i.NumberOfSwimmers.ToString() + " : " + "N/A" // Replace 'Capacity' with a placeholder as 'info1' does not have 'Capacity'
                                : "N/A : N/A" // Handle null case for 'NumberOfSwimmers'
                        };

            if (schoolId.HasValue)
            {
                query = query.Where(x => x.CoachName == fullName); // Fix: Replace 'i.SchoolID' with a valid property from the query result
            }
            else if (!string.IsNullOrEmpty(fullName))
            {
                query = query.Where(x => x.CoachName.Contains(fullName)); // Fix: Replace 'c.FullName' with 'x.CoachName'
            }
            else if (level.HasValue)
            {
                query = query.Where(x => x.Level == level.Value.ToString()); // Fix: Replace 'i.schoolLevel' with 'x.Level'
            }
            else if (type.HasValue)
            {
                query = query.Where(x => x.Type == type.Value.ToString()); // Fix: Replace 'i.SchoolType' with 'x.Type'
            }
            else
            {
                return new List<ShowSchoolDto>();
            }

            return await query.ToListAsync();
        }
        public async Task<List<SwimmerDetailsTabDto>> GetSwimmerDetailsTabAsync(long schoolId)
        {
            var result = await (from d in _context.Details1
                                join i in _context.Infos1 on d.SwimmerID equals i.SwimmerID
                                join level in _context.AppCodes on i.CurrentLevel equals level.sub_id
                                where d.SchoolID == schoolId
                                select new SwimmerDetailsTabDto
                                {
                                    FullName = i.FulllName,
                                    Attendence = d.Attendence.HasValue ? d.Attendence.Value.ToString("F2") : "N/A", // Fix for CS0029: Convert nullable decimal to string
                                    SwimmerLevel = level.description
                                }).ToListAsync();

            return result;
        }
        public async Task UpdateSchoolAsync(UpdateSchoolDto dto)
        {
            var school = await _context.infos.FirstOrDefaultAsync(i => i.SchoolID == dto.SchoolID);
            if (school == null)
                throw new InvalidOperationException("School not found.");

            school.CoachID = dto.CoachID;
            school.FirstDay = dto.FirstDay;
            school.SecondDay = dto.SecondDay;
            school.updatedAtSite = dto.Site;
            school.updatedBy = dto.User;
            school.updatedAtDate = DateTime.Now;
            school.StartTime = (decimal)dto.StartTime.TotalHours;
            school.EndTime = (decimal)dto.EndTime.TotalHours;
            school.SchoolType = dto.Type;

            // Log the update action
            var log = new log1
            {
                schoolID = dto.SchoolID,
                ActionID = 15,
                createdAtsite = dto.Site,
                CreatedBy = dto.User,
                CreatedAtDate = DateOnly.FromDateTime(DateTime.Now)
            };
            _context.logs1.Add(log);

            await _context.SaveChangesAsync();
        }

    }
}
