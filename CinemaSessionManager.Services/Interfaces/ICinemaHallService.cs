using CinemaSessionManager.Models.Enums;
using CinemaSessionManager.Services.Dtos;

namespace CinemaSessionManager.Services.Interfaces
{
    public interface ICinemaHallService
    {
        Task<List<CinemaHallListDto>> GetAllHallsAsync();
        Task<CinemaHallDetailDto?> GetHallDetailsAsync(int hallId);
        Task<CinemaHallDetailDto> CreateHallAsync(string name, int seatsCount, CinemaHallType hallType);
        Task UpdateHallAsync(int id, string name, int seatsCount, CinemaHallType hallType);
        Task DeleteHallAsync(int id);
    }
}
