using CinemaSessionManager.Models.Entities;
using CinemaSessionManager.Models.Enums;

namespace CinemaSessionManager.Services
{
    /// <summary>
    /// Штучне сховище даних з реалістичними початковими значеннями.
    /// Доступ до цього класу має лише CinemaService (internal).
    /// Містить 3 кінозали і 14 кіносеансів.
    /// </summary>
    internal static class FakeDataStore
    {
        private static readonly List<CinemaHallEntity> _cinemaHalls;
        private static readonly List<SessionEntity> _sessions;

        static FakeDataStore()
        {
            _cinemaHalls = InitializeCinemaHalls();
            _sessions = InitializeSessions();
        }

        internal static List<CinemaHallEntity> GetCinemaHalls()
        {
            return new List<CinemaHallEntity>(_cinemaHalls);
        }

        internal static List<SessionEntity> GetSessionsByHallId(int hallId)
        {
            var result = new List<SessionEntity>();
            foreach (var session in _sessions)
            {
                if (session.CinemaHallId == hallId)
                {
                    result.Add(session);
                }
            }
            return result;
        }

        internal static List<SessionEntity> GetAllSessions()
        {
            return new List<SessionEntity>(_sessions);
        }

        private static List<CinemaHallEntity> InitializeCinemaHalls()
        {
            return new List<CinemaHallEntity>
            {
                new CinemaHallEntity(1, "Зал Преміум", 200, CinemaHallType.IMAX),
                new CinemaHallEntity(2, "Зал Комфорт", 120, CinemaHallType.ThreeD),
                new CinemaHallEntity(3, "Малий зал", 60, CinemaHallType.TwoD)
            };
        }

        /// <summary>
        /// 10 сеансів для залу 1, 2 сеанси для залу 2, 2 сеанси для залу 3.
        /// </summary>
        private static List<SessionEntity> InitializeSessions()
        {
            var today = DateTime.Today;

            return new List<SessionEntity>
            {
                // --- Зал 1: "Зал Преміум" (IMAX) — 10 сеансів ---
                new SessionEntity(1, 1, "Дюна: Частина друга", MovieGenre.SciFi,
                    2024, today.AddHours(9).AddMinutes(30), 166),

                new SessionEntity(2, 1, "Опенгеймер", MovieGenre.Drama,
                    2023, today.AddHours(12).AddMinutes(30), 180),

                new SessionEntity(3, 1, "Інтерстеллар", MovieGenre.SciFi,
                    2014, today.AddHours(16), 169),

                new SessionEntity(4, 1, "Аватар: Шлях води", MovieGenre.Action,
                    2022, today.AddHours(19).AddMinutes(30), 192),

                new SessionEntity(5, 1, "Темний лицар", MovieGenre.Action,
                    2008, today.AddHours(23), 152),

                new SessionEntity(6, 1, "Початок", MovieGenre.Thriller,
                    2010, today.AddHours(10), 148),

                new SessionEntity(7, 1, "Віднесені привидами", MovieGenre.Animation,
                    2001, today.AddHours(13), 125),

                new SessionEntity(8, 1, "Матриця", MovieGenre.SciFi,
                    1999, today.AddHours(15).AddMinutes(30), 136),

                new SessionEntity(9, 1, "Форрест Гамп", MovieGenre.Drama,
                    1994, today.AddHours(18), 142),

                new SessionEntity(10, 1, "Один вдома", MovieGenre.Comedy,
                    1990, today.AddHours(20).AddMinutes(30), 103),

                // --- Зал 2: "Зал Комфорт" (3D) — 2 сеанси ---
                new SessionEntity(11, 2, "Крижане серце", MovieGenre.Animation,
                    2013, today.AddHours(11), 102),

                new SessionEntity(12, 2, "Шрек", MovieGenre.Comedy,
                    2001, today.AddHours(14), 90),

                // --- Зал 3: "Малий зал" (2D) — 2 сеанси ---
                new SessionEntity(13, 3, "Воно", MovieGenre.Horror,
                    2017, today.AddHours(21), 135),

                new SessionEntity(14, 3, "Титанік", MovieGenre.Romance,
                    1997, today.AddHours(17), 195)
            };
        }
    }
}
