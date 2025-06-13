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
                FulllName = dto.FullName, // Correct property name
                BirthDate = DateOnly.FromDateTime(dto.BirthDate), // Convert DateTime to DateOnly
                StartLevel = dto.StartLevel,
                CurrentLevel = dto.StartLevel,
                Site = dto.Site,
                Gender = dto.Gender,
                CLub = dto.Club,
                CreatedAtSite = dto.Site,
                CreatedBy = dto.UserID,
                createdAtDate = now // Fix for CS0029: Use DateTime directly instead of DateOnly
            };
            _context.Infos1.Add(swimmerInfo);
            await _context.SaveChangesAsync();

            // 2. Insert into Swimmers.Parent
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

            // 3. Insert into Swimmers.log
            var swimmerLog = new log2
            {
                swimmerID = swimmerInfo.SwimmerID,
                ActionID = 1,
                createdAtsite = dto.Site,
                CreatedBy = dto.UserID,
                CreatedAtDate = DateOnly.FromDateTime(now) // Fix for CS0029: Use DateTime directly instead of DateOnly
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
        public async Task<SwimmerInfoTabDto?> GetSwimmerInfoTabAsync(long swimmerId)
        {
            var result = await (from si in _context.Infos1
                                join sp in _context.Parents on si.SwimmerID equals sp.SwimmerID
                                join cLevel in _context.AppCodes on si.CurrentLevel equals cLevel.sub_id
                                join sLevel in _context.AppCodes on si.StartLevel equals sLevel.sub_id
                                join club in _context.AppCodes on si.CLub equals club.sub_id
                                join site in _context.AppCodes on si.Site equals site.sub_id
                                where si.SwimmerID == swimmerId
                                select new SwimmerInfoTabDto
                                {
                                    FullName = si.FulllName, // Fix for CS1061: Correct property name from 'FullName' to 'FulllName'
                                    BirthDate = si.BirthDate.ToDateTime(TimeOnly.MinValue), // Convert DateOnly to DateTime
                                    Site = site.description,
                                    CurrentLevel = cLevel.description,
                                    StartLevel = sLevel.description,
                                    CreatedAtDate = si.createdAtDate,
                                    PrimaryJop = sp.PrimaryJop,
                                    SecondaryJop = sp.SecondaryJop,
                                    PrimaryPhone = sp.PrimaryPhone,
                                    SecondaryPhone = sp.SecondaryPhone,
                                    Club = club.description
                                }).FirstOrDefaultAsync();

            return result;
        }
        public async Task<List<SwimmerLogTabDto>> GetSwimmerLogTabAsync(long swimmerId)
        {
            var result = await (from l in _context.logs2
                                join ac in _context.Actions on l.ActionID equals ac.ActionID
                                join u in _context.users on l.CreatedBy equals u.userid
                                join acc in _context.AppCodes on l.createdAtsite equals acc.sub_id
                                where l.swimmerID == swimmerId
                                select new SwimmerLogTabDto
                                {
                                    ActionName = ac.ActionName,
                                    UserFullName = u.fullname,
                                    CreatedAtDate = l.CreatedAtDate.ToDateTime(TimeOnly.MinValue), // Fix: Convert DateOnly to DateTime
                                    Site = acc.description
                                }).ToListAsync();

            return result;
        }
        public async Task<object?> GetSwimmerTechnicalTabAsync(long swimmerId)
        {
            var tech = await _context.Technicals
                .FirstOrDefaultAsync(t => t.SwimmerID == swimmerId);

            if (tech == null)
                return null;

            // School Technical Tab  
            if (tech.ISSchool.HasValue && tech.ISSchool.Value)
            {
                var result = await (from st in _context.Technicals
                                    join sd in _context.Details1 on st.SwimmerID equals sd.SwimmerID
                                    join si in _context.Infos1 on sd.SchoolID equals si.SwimmerID
                                    join c in _context.Coaches on sd.CoachID equals c.CoachID
                                    join ac in _context.AppCodes on st.CurrentLevel equals ac.sub_id
                                    where st.SwimmerID == swimmerId
                                    select new SwimmerTechnicalSchoolTabDto
                                    {
                                        CoachName = c.FullName,
                                        FirstDay = si.FirstDay,
                                        SecondDay = si.SecondDay,
                                        StartTime = TimeSpan.FromHours((double)sd.StartTime), // Fix: Replace 'si.StartTime' with 'sd.StartTime'
                                        EndTime = TimeSpan.FromHours((double)sd.EndTime), // Fix: Replace 'si.EndTime' with 'sd.EndTime'
                                        SwimmerLevel = ac.description,
                                        Attendence = !string.IsNullOrEmpty(sd.Attendence) ? sd.Attendence : "N/A"
                                    }).FirstOrDefaultAsync();

                return result;
            }

            // PreTeam Technical Tab  
            else if (tech.ISPreTeam.HasValue && tech.ISPreTeam.Value)
            {
                var result = await (from st in _context.Technicals
                                    join pd in _context.Details on st.SwimmerID equals pd.SwimmerID
                                    join pi in _context.Infos1 on pd.PTeamID equals pi.SwimmerID
                                    join c in _context.Coaches on pd.CoachID equals c.CoachID
                                    join ac in _context.AppCodes on st.CurrentLevel equals ac.sub_id
                                    join acc in _context.AppCodes on pd.LastStar equals acc.sub_id into accJoin
                                    from acc in accJoin.DefaultIfEmpty()
                                    where st.SwimmerID == swimmerId
                                    select new SwimmerTechnicalPreTeamTabDto
                                    {
                                        CoachName = c.FullName,
                                        FirstDay = pi.FirstDay,
                                        SecondDay = pi.SecondDay,
                                        ThirdDay = pi.ThirdDay,
                                        StartTime = TimeSpan.FromHours((double)pd.StartTime), // Fix: Replace 'pi.StartTime' with 'pd.StartTime'
                                        EndTime = TimeSpan.FromHours((double)pd.EndTime), // Fix: Replace 'pi.EndTime' with 'pd.EndTime'
                                        SwimmerLevel = ac.description,
                                        Attendence = !string.IsNullOrEmpty(pd.Attendence) ? pd.Attendence : "N/A",
                                        LastStar = acc != null ? acc.description : null
                                    }).FirstOrDefaultAsync();

                return result;
            }

            // If neither, return null  
            return null;
        }
        public async Task<List<ActionNameDto>> SearchActionsAsync(int userId, long swimmerId, short userSite)
        {
            // 1. Get userTypeID
            var userTypeId = await _context.users
                .Where(u => u.userid == userId)
                .Select(u => u.UserTypeID)
                .FirstOrDefaultAsync();

            // 2. Get SwimmerSite
            var swimmerSite = await _context.Infos1
                .Where(i => i.SwimmerID == swimmerId)
                .Select(i => i.Site)
                .FirstOrDefaultAsync();

            // 3. Build query based on site comparison
            bool sameSite = userSite == swimmerSite;

            var actionsQuery = from up in _context.Users_Privs
                               join a in _context.Actions on up.ActionID equals a.ActionID
                               where up.UserTypeID == userTypeId
                                     && a.Module == "Mod_SW"
                                     && a.SameSite == (sameSite ? true : false)
                                     && a.ActionID != 1
                               select new ActionNameDto
                               {
                                   ActionName = a.ActionName
                               };

            var actions = await actionsQuery
                .OrderBy(a => a.ActionName)
                .ToListAsync();

            return actions;
        }
        public async Task<List<ShowSwimmerDto>> ShowSwimmersAsync(long? swimmerId, string? fullName, string? year, short? level)
        {
            var query = from i in _context.Infos1
                        join ac in _context.AppCodes on i.CurrentLevel equals ac.sub_id
                        join acc in _context.AppCodes on i.Site equals acc.sub_id
                        join accc in _context.AppCodes on i.CLub equals accc.sub_id
                        // School coach
                        join sd in _context.Details1 on i.SwimmerID equals sd.SwimmerID into sdJoin
                        from sd in sdJoin.DefaultIfEmpty()
                        join scCoach in _context.Coaches on sd.CoachID equals scCoach.CoachID into scCoachJoin
                        from scCoach in scCoachJoin.DefaultIfEmpty()
                            // PreTeam coach
                        join ptd in _context.Details on i.SwimmerID equals ptd.SwimmerID into ptdJoin
                        from ptd in ptdJoin.DefaultIfEmpty()
                        join ptCoach in _context.Coaches on ptd.CoachID equals ptCoach.CoachID into ptCoachJoin
                        from ptCoach in ptCoachJoin.DefaultIfEmpty()
                        select new ShowSwimmerDto
                        {
                            FullName = i.FulllName,
                            Year = i.BirthDate.Year,
                            CurrentLevel = ac.description,
                            CoachName = scCoach != null && !string.IsNullOrEmpty(scCoach.FullName)
                                ? scCoach.FullName
                                : (ptCoach != null ? ptCoach.FullName : null),
                            Site = acc.description,
                            Club = accc.description
                        };

            if (swimmerId.HasValue)
            {
                query = query.Where(x => x.FullName != null && _context.Infos1.Any(i => i.SwimmerID == swimmerId.Value && i.FulllName == x.FullName));
            }
            else if (!string.IsNullOrEmpty(fullName))
            {
                query = query.Where(x => x.FullName.Contains(fullName));
            }
            else if (!string.IsNullOrEmpty(year) && int.TryParse(year, out int y))
            {
                query = query.Where(x => x.Year == y);
            }
            else if (level.HasValue)
            {
                query = query.Where(x => _context.Infos1.Any(i => i.FulllName == x.FullName && i.CurrentLevel == level.Value));
            }
            else
            {
                return new List<ShowSwimmerDto>();
            }

            return await query.ToListAsync();
        }
        public async Task UpdateSwimmerAsync(UpdateSwimmerDto dto)
        {
            // 1. Update Swimmers.Info
            var swimmerInfo = await _context.Infos1.FirstOrDefaultAsync(s => s.SwimmerID == dto.SwimmerID);
            if (swimmerInfo == null)
                throw new InvalidOperationException("Swimmer not found.");

            swimmerInfo.FulllName = dto.FullName;
            swimmerInfo.BirthDate = DateOnly.FromDateTime(dto.BirthDate);
            swimmerInfo.Gender = dto.Gender;
            swimmerInfo.CLub = dto.Club;
            swimmerInfo.UpdatedAtSite = dto.Site;
            swimmerInfo.UpdatedBy = dto.UserID;
            swimmerInfo.UpdatedAtDate = DateTime.Now;

            // 2. Update Swimmers.Parent
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.SwimmerID == dto.SwimmerID);
            if (parent != null)
            {
                parent.PrimaryPhone = dto.PrimaryPhone;
                parent.SecondaryPhone = dto.SecondaryPhone;
                parent.PrimaryJop = dto.PrimaryJop;
                parent.SecondaryJop = dto.SecondaryJop;
                parent.Email = dto.Email;
                parent.Address = dto.Address;
            }

            // 3. Insert into Swimmers.log
            var log = new log2
            {
                swimmerID = dto.SwimmerID,
                ActionID = 3,
                createdAtsite = dto.Site,
                CreatedBy = dto.UserID,
                CreatedAtDate = DateOnly.FromDateTime(DateTime.Now)
            };
            _context.logs2.Add(log);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateSwimmerLevelAsync(UpdateSwimmerLevelDto dto)
        {
            // 1. Update Swimmers.Info
            var swimmerInfo = await _context.Infos1.FirstOrDefaultAsync(s => s.SwimmerID == dto.SwimmerID);
            if (swimmerInfo != null)
            {
                swimmerInfo.CurrentLevel = dto.NewLevel;
                swimmerInfo.UpdatedBy = dto.UserID;
            }

            // 2. Update Swimmers.Technical
            var technicals = await _context.Technicals
                .Where(t => t.SwimmerID == dto.SwimmerID)
                .ToListAsync();
            foreach (var tech in technicals)
            {
                tech.CurrentLevel = dto.NewLevel;
                tech.UpdatedBy = dto.UserID;
            }

            // 3. Insert into Swimmers.log
            var log = new log2
            {
                swimmerID = dto.SwimmerID,
                ActionID = 4,
                createdAtsite = dto.Site,
                CreatedBy = dto.UserID,
                CreatedAtDate = DateOnly.FromDateTime(DateTime.Now)
            };
            _context.logs2.Add(log);

            await _context.SaveChangesAsync();
        }
        public async Task<ViewPossibleSchoolResultDto> ViewPossibleSchoolAsync(long swimmerId, short type)
        {
            // 1. Get swimmer's site, level, and expiry date
            var swimmer = await _context.Infos1
                .Where(s => s.SwimmerID == swimmerId)
                .Select(s => new { s.Site, s.CurrentLevel })
                .FirstOrDefaultAsync();
            if (swimmer == null)
                throw new InvalidOperationException("Swimmer not found.");

            var tech = await _context.Technicals
                .Where(t => t.SwimmerID == swimmerId)
                .Select(t => t.ExpiryDate)
                .FirstOrDefaultAsync();

            var now = DateOnly.FromDateTime(DateTime.Now); // Convert DateTime to DateOnly for comparison

            var schools = await (from i in _context.infos
                                 join c in _context.Coaches on i.CoachID equals c.CoachID
                                 where i.SchoolType == type
                                       && i.site == swimmer.Site
                                       && i.NumberOfSwimmers != null && int.Parse(i.NumberOfSwimmers) < int.Parse(i.MaxNumber)
                                       && i.schoolLevel == swimmer.CurrentLevel
                                 select new PossibleSchoolDto
                                 {
                                     CoachName = c.FullName,
                                     Days = i.FirstDay + " and " + i.SecondDay,
                                     FromTo = TimeSpan.FromHours((double)i.StartTime).ToString(@"hh\:mm") + " : " +
                                              TimeSpan.FromHours((double)i.EndTime).ToString(@"hh\:mm")
                                 }).ToListAsync();

            // 3. Get invoice items
            List<InvoiceItemDto> invoices;
            if (tech != null && tech.Value >= now.AddMonths(1)) // Fix: Use DateOnly for comparison
            {
                invoices = await (from item in _context.Invoice_Items
                                  where item.ProductID == type && item.Site == swimmer.Site
                                  select new InvoiceItemDto
                                  {
                                      ItemName = item.ItemName,
                                      DurationInMonths = item.DurationInMonths ?? 0, // Fix for CS0266 and CS8629: Use null-coalescing operator to handle nullable type
                                      Amount = 0.00m
                                  }).ToListAsync();
            }
            else
            {
                invoices = await (from item in _context.Invoice_Items
                                  where item.ProductID == type && item.Site == swimmer.Site
                                  select new InvoiceItemDto
                                  {
                                      ItemName = item.ItemName,
                                      DurationInMonths = item.DurationInMonths ?? 0, // Fix for CS0266 and CS8629: Use null-coalescing operator to handle nullable type
                                      Amount = item.Amount
                                  }).ToListAsync();
            }

            return new ViewPossibleSchoolResultDto
            {
                Schools = schools,
                Invoices = invoices
            };
        }
    }
}
