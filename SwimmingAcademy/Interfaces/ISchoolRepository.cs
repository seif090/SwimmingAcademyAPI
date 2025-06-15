using SwimmingAcademy.DTOs;

namespace SwimmingAcademy.Interfaces
{
    public interface ISchoolRepository
    {
        Task<long> CreateSchoolAsync(CreateSchoolRequest request);
        Task<IEnumerable<SchoolSearchResultDto>> SearchSchoolsAsync(SchoolSearchRequest request);
        Task<bool> UpdateSchoolAsync(UpdateSchoolRequest request);
        Task<bool> EndSchoolAsync(EndSchoolRequest request);
        Task<SchoolDetailsTabDto?> GetSchoolDetailsTabAsync(long schoolID);
        Task<IEnumerable<SchoolSwimmerDetailsDto>> GetSchoolSwimmerDetailsAsync(long schoolID);
        Task<IEnumerable<ActionNameDto>> SearchSchoolActionsAsync(SchoolActionSearchRequest request);
        Task<ViewPossibleSchoolResponse> GetPossibleSchoolOptionsAsync(long swimmerID, short type);

    }
}
