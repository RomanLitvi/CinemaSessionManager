using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CinemaSessionManager.MauiApp.ViewModels
{
    /// <summary>
    /// Базовий клас для всіх ViewModel. Реалізує INotifyPropertyChanged
    /// для підтримки прив'язки даних у MVVM-патерні.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Сповіщає UI про зміну властивості.
        /// </summary>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Встановлює значення поля і сповіщає UI лише якщо значення змінилось.
        /// Повертає true, якщо значення змінилось.
        /// </summary>
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
