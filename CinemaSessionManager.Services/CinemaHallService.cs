using CinemaSessionManager.Models.Entities;
using CinemaSessionManager.Models.Enums;
using CinemaSessionManager.Repositories.Interfaces;
using CinemaSessionManager.Services.Dtos;
using CinemaSessionManager.Services.Interfaces;

namespace CinemaSessionManager.Services
{
    public class CinemaHallService : ICinemaHallService
    {
        private readonly ICinemaHallRepository _hallRepository;
        private readonly ISessionRepository _sessionRepository;

        public CinemaHallService(ICinemaHallRepository hallRepository, ISessionRepository sessionRepository)
        {
            _hallRepository = hallRepository;
            _sessionRepository = sessionRepository;
        }

        public async Task<List<CinemaHallListDto>> GetAllHallsAsync()
        {
            var halls = await _hallRepository.GetAllAsync();
            var result = new List<CinemaHallListDto>();

            foreach (var hall in halls)
            {
                var sessions = await _sessionRepository.GetByHallIdAsync(hall.Id);
                result.Add(new CinemaHallListDto
                {
                    Id = hall.Id,
                    Name = hall.Name,
                    HallType = hall.HallType,
                    SeatsCount = hall.SeatsCount,
                    SessionCount = sessions.Count
                });
            }

            return result;
        }

        public async Task<CinemaHallDetailDto?> GetHallDetailsAsync(int hallId)
        {
            var hall = await _hallRepository.GetByIdAsync(hallId);
            if (hall == null)
                return null;

            var sessions = await _sessionRepository.GetByHallIdAsync(hallId);
            var sessionDtos = sessions.Select(s => new SessionListDto
            {
                Id = s.Id,
                MovieTitle = s.MovieTitle,
                Genre = s.Genre,
                ReleaseYear = s.ReleaseYear,
                StartTime = s.StartTime,
                DurationMinutes = s.DurationMinutes
            }).ToList();

            return new CinemaHallDetailDto
            {
                Id = hall.Id,
                Name = hall.Name,
                HallType = hall.HallType,
                SeatsCount = hall.SeatsCount,
                Sessions = sessionDtos
            };
        }

        public async Task<CinemaHallDetailDto> CreateHallAsync(string name, int seatsCount, CinemaHallType hallType)
        {
            int newId = await _hallRepository.GenerateNextIdAsync();
            var entity = new CinemaHallEntity(newId, name, seatsCount, hallType);
            await _hallRepository.AddAsync(entity);

            return new CinemaHallDetailDto
            {
                Id = entity.Id,
                Name = entity.Name,
                HallType = entity.HallType,
                SeatsCount = entity.SeatsCount,
                Sessions = new List<SessionListDto>()
            };
        }

        public async Task UpdateHallAsync(int id, string name, int seatsCount, CinemaHallType hallType)
        {
            var entity = new CinemaHallEntity(id, name, seatsCount, hallType);
            await _hallRepository.UpdateAsync(entity);
        }

        public async Task DeleteHallAsync(int id)
        {
            await _sessionRepository.DeleteByHallIdAsync(id);
            await _hallRepository.DeleteAsync(id);
        }
    }
}
