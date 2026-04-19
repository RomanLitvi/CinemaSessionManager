using System.Text.Json;
using System.Text.Json.Serialization;
using CinemaSessionManager.Models.Entities;
using CinemaSessionManager.Models.Enums;

namespace CinemaSessionManager.Repositories.Storage
{
    internal class JsonDataStore : IDataStore
    {
        private readonly string _filePath;
        private readonly SemaphoreSlim _lock = new(1, 1);
        private CinemaStoreData _cache;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public JsonDataStore(string dataDirectoryPath)
        {
            Directory.CreateDirectory(dataDirectoryPath);
            _filePath = Path.Combine(dataDirectoryPath, "cinema_data.json");
            _cache = LoadFromFile();
        }

        private CinemaStoreData LoadFromFile()
        {
            if (!File.Exists(_filePath))
            {
                var data = CreateSeedData();
                var json = JsonSerializer.Serialize(data, JsonOptions);
                File.WriteAllText(_filePath, json);
                return data;
            }

            var fileJson = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<CinemaStoreData>(fileJson, JsonOptions)
                   ?? CreateSeedData();
        }

        private async Task PersistAsync()
        {
            var json = JsonSerializer.Serialize(_cache, JsonOptions);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task<List<CinemaHallEntity>> GetAllCinemaHallsAsync()
        {
            await _lock.WaitAsync();
            try
            {
                return _cache.CinemaHalls.ToList();
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task<CinemaHallEntity?> GetCinemaHallByIdAsync(int id)
        {
            await _lock.WaitAsync();
            try
            {
                return _cache.CinemaHalls.FirstOrDefault(h => h.Id == id);
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task AddCinemaHallAsync(CinemaHallEntity hall)
        {
            await _lock.WaitAsync();
            try
            {
                _cache.CinemaHalls.Add(hall);
                await PersistAsync();
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task UpdateCinemaHallAsync(CinemaHallEntity hall)
        {
            await _lock.WaitAsync();
            try
            {
                var existing = _cache.CinemaHalls.FirstOrDefault(h => h.Id == hall.Id);
                if (existing != null)
                {
                    existing.Name = hall.Name;
                    existing.SeatsCount = hall.SeatsCount;
                    existing.HallType = hall.HallType;
                    await PersistAsync();
                }
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task DeleteCinemaHallAsync(int id)
        {
            await _lock.WaitAsync();
            try
            {
                _cache.CinemaHalls.RemoveAll(h => h.Id == id);
                _cache.Sessions.RemoveAll(s => s.CinemaHallId == id);
                await PersistAsync();
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task<List<SessionEntity>> GetSessionsByHallIdAsync(int hallId)
        {
            await _lock.WaitAsync();
            try
            {
                return _cache.Sessions.Where(s => s.CinemaHallId == hallId).ToList();
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task<SessionEntity?> GetSessionByIdAsync(int id)
        {
            await _lock.WaitAsync();
            try
            {
                return _cache.Sessions.FirstOrDefault(s => s.Id == id);
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task AddSessionAsync(SessionEntity session)
        {
            await _lock.WaitAsync();
            try
            {
                _cache.Sessions.Add(session);
                await PersistAsync();
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task UpdateSessionAsync(SessionEntity session)
        {
            await _lock.WaitAsync();
            try
            {
                var existing = _cache.Sessions.FirstOrDefault(s => s.Id == session.Id);
                if (existing != null)
                {
                    existing.MovieTitle = session.MovieTitle;
                    existing.Genre = session.Genre;
                    existing.ReleaseYear = session.ReleaseYear;
                    existing.StartTime = session.StartTime;
                    existing.DurationMinutes = session.DurationMinutes;
                    existing.CinemaHallId = session.CinemaHallId;
                    await PersistAsync();
                }
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task DeleteSessionAsync(int id)
        {
            await _lock.WaitAsync();
            try
            {
                _cache.Sessions.RemoveAll(s => s.Id == id);
                await PersistAsync();
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task DeleteSessionsByHallIdAsync(int hallId)
        {
            await _lock.WaitAsync();
            try
            {
                _cache.Sessions.RemoveAll(s => s.CinemaHallId == hallId);
                await PersistAsync();
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task<int> GenerateNextHallIdAsync()
        {
            await _lock.WaitAsync();
            try
            {
                int nextId = _cache.NextHallId;
                _cache.NextHallId++;
                await PersistAsync();
                return nextId;
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task<int> GenerateNextSessionIdAsync()
        {
            await _lock.WaitAsync();
            try
            {
                int nextId = _cache.NextSessionId;
                _cache.NextSessionId++;
                await PersistAsync();
                return nextId;
            }
            finally
            {
                _lock.Release();
            }
        }

        private static CinemaStoreData CreateSeedData()
        {
            var today = DateTime.Today;

            return new CinemaStoreData
            {
                CinemaHalls = new List<CinemaHallEntity>
                {
                    new(1, "Зал Преміум", 200, CinemaHallType.IMAX),
                    new(2, "Зал Комфорт", 120, CinemaHallType.ThreeD),
                    new(3, "Малий зал", 60, CinemaHallType.TwoD)
                },
                Sessions = new List<SessionEntity>
                {
                    new(1, 1, "Дюна: Частина друга", MovieGenre.SciFi, 2024, today.AddHours(9).AddMinutes(30), 166),
                    new(2, 1, "Опенгеймер", MovieGenre.Drama, 2023, today.AddHours(12).AddMinutes(30), 180),
                    new(3, 1, "Інтерстеллар", MovieGenre.SciFi, 2014, today.AddHours(16), 169),
                    new(4, 1, "Аватар: Шлях води", MovieGenre.Action, 2022, today.AddHours(19).AddMinutes(30), 192),
                    new(5, 1, "Темний лицар", MovieGenre.Action, 2008, today.AddHours(23), 152),
                    new(6, 1, "Початок", MovieGenre.Thriller, 2010, today.AddHours(10), 148),
                    new(7, 1, "Віднесені привидами", MovieGenre.Animation, 2001, today.AddHours(13), 125),
                    new(8, 1, "Матриця", MovieGenre.SciFi, 1999, today.AddHours(15).AddMinutes(30), 136),
                    new(9, 1, "Форрест Гамп", MovieGenre.Drama, 1994, today.AddHours(18), 142),
                    new(10, 1, "Один вдома", MovieGenre.Comedy, 1990, today.AddHours(20).AddMinutes(30), 103),
                    new(11, 2, "Крижане серце", MovieGenre.Animation, 2013, today.AddHours(11), 102),
                    new(12, 2, "Шрек", MovieGenre.Comedy, 2001, today.AddHours(14), 90),
                    new(13, 3, "Воно", MovieGenre.Horror, 2017, today.AddHours(21), 135),
                    new(14, 3, "Титанік", MovieGenre.Romance, 1997, today.AddHours(17), 195)
                },
                NextHallId = 4,
                NextSessionId = 15
            };
        }

        private class CinemaStoreData
        {
            public List<CinemaHallEntity> CinemaHalls { get; set; } = new();
            public List<SessionEntity> Sessions { get; set; } = new();
            public int NextHallId { get; set; } = 1;
            public int NextSessionId { get; set; } = 1;
        }
    }
}
