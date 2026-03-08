using CinemaSessionManager.ViewModels;

namespace CinemaSessionManager.MauiApp.Pages
{
    /// <summary>
    /// Code-Behind для сторінки деталей сеансу.
    /// Отримує SessionViewModel та назву залу як параметри і заповнює UI.
    /// Навігація "назад" відбувається автоматично через NavigationPage (кнопка "Назад" у заголовку).
    /// </summary>
    public partial class SessionDetailsPage : ContentPage
    {
        public SessionDetailsPage(SessionViewModel session, string hallName)
        {
            InitializeComponent();

            // Заголовок сторінки у панелі навігації
            Title = session.MovieTitle;

            // Заповнення UI-елементів даними з ViewModel
            MovieTitleLabel.Text = $"🎬 {session.MovieTitle}";
            HallNameLabel.Text = hallName;
            GenreLabel.Text = session.Genre.ToString();
            ReleaseYearLabel.Text = session.ReleaseYear.ToString();
            StartTimeLabel.Text = session.StartTime.ToString("HH:mm");
            DurationLabel.Text = $"{session.DurationMinutes} хв";
            EndTimeLabel.Text = session.EndTime.ToString("HH:mm");
        }
    }
}
