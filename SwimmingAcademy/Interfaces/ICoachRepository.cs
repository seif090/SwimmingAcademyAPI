using SwimmingAcademy.DTOs;

namespace SwimmingAcademy.Interfaces
{
    public interface ICoachRepository
    {
        /// <summary>
        /// Gets a list of free coaches based on the provided filter.
        /// </summary>
        /// <param name="request">Filter criteria for free coaches.</param>
        /// <returns>List of free coaches.</returns>
        Task<IEnumerable<FreeCoachDto>> GetFreeCoachesAsync(FreeCoachFilterRequest request);

        /// <summary>
        /// Gets a coach by their unique ID.
        /// </summary>
        /// <param name="id">Coach ID.</param>
        /// <returns>CoachDTO if found, otherwise null.</returns>
        Task<CoachDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new coach.
        /// </summary>
        /// <param name="dto">Coach data.</param>
        /// <returns>The created coach's ID.</returns>
        Task<int> CreateCoachAsync(CoachDTO dto);

        /// <summary>
        /// Updates an existing coach.
        /// </summary>
        /// <param name="dto">Coach data.</param>
        /// <returns>True if update succeeded, false otherwise.</returns>
        Task<bool> UpdateCoachAsync(CoachDTO dto);

        /// <summary>
        /// Deletes a coach by ID.
        /// </summary>
        /// <param name="coachId">Coach ID.</param>
        /// <returns>True if delete succeeded, false otherwise.</returns>
        Task<bool> DeleteCoachAsync(int coachId);
    }
}