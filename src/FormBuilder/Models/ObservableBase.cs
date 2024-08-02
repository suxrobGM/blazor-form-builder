using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FormBuilder.Models;

/// <summary>
/// Base class for observable models.
/// It implements INotifyPropertyChanged.
/// </summary>
public abstract class ObservableBase : INotifyPropertyChanged
{
    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName"></param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Sets the field value and raises the PropertyChanged event if the value has changed.
    /// </summary>
    /// <param name="field">The field to set.</param>
    /// <param name="value">The value to set.</param>
    /// <param name="propertyName">The property name. Default is the caller member name.</param>
    /// <typeparam name="T">The type of the field.</typeparam>
    /// <returns>Returns true if the value has changed, otherwise false.</returns>
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }
        
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
