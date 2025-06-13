using AutoMapper;
using SwimmingAcademy.DTOs;
using SwimmingAcademy.Repositories.Interfaces;
using SwimmingAcademy.Services.Interfaces;

namespace SwimmingAcademy.Services
{
    public class SwimmerService : ISwimmerService
    {
        private readonly ISwimmerRepository _repository;
        private readonly IMapper _mapper;

        public SwimmerService(ISwimmerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
    }
}
