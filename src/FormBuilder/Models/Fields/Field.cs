using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text.Json.Serialization;
using FormBuilder.Converters;
using FormBuilder.Utils;

namespace FormBuilder.Models;

/// <summary>
/// Represents a model for the form field.
/// </summary>
[JsonConverter(typeof(FieldJsonConverter))]
public abstract class Field : ObservableBase
{
    protected Field()
    {
        Validators.CollectionChanged += HandleValidatorsCollectionChanged;
    }
    
    /// <summary>
    /// Field name. If not provided, it will be generated.
    /// </summary>
    private string? _name;
    public string Name
    {
        get
        {
            if (string.IsNullOrEmpty(_name))
            {
                _name = Generator.GenerateShortId($"{Type}_".ToLower());
            }

            return _name;
        }
        set => SetField(ref _name, value);
    }

    /// <summary>
    /// Field label.
    /// </summary>
    private string? _label;
    public string? Label
    {
        get => _label;
        set => SetField(ref _label, value);
    }

    /// <summary>
    /// Input placeholder.
    /// </summary>
    private string? _placeholder;
    public string? Placeholder
    {
        get => _placeholder;
        set => SetField(ref _placeholder, value);
    }
    
    /// <summary>
    /// Field type such as TextField, NumericIntField, NumericDecimalField, SelectField, DateField.
    /// </summary>
    public abstract FieldType Type { get; }

    /// <summary>
    /// Whether the field is read-only.
    /// </summary>
    private bool _readOnly;
    public bool ReadOnly
    {
        get => _readOnly;
        set => SetField(ref _readOnly, value);
    }
    
    /// <summary>
    /// Whether the field is disabled.
    /// </summary>
    private bool _disabled;
    public bool Disabled
    {
        get => _disabled;
        set => SetField(ref _disabled, value);
    }
    
    /// <summary>
    /// The hint text to be displayed below the field.
    /// </summary>
    private string? _hint;
    public string? Hint
    {
        get => _hint;
        set => SetField(ref _hint, value);
    }
    
    /// <summary>
    /// List of validators to be applied to the field.
    /// </summary>
    public ObservableCollection<Validator> Validators { get; set; } = [];
    
    private void HandleValidatorsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
    {
        if (args is { Action: NotifyCollectionChangedAction.Add, NewItems: not null })
        {
            foreach (Validator validator in args.NewItems)
            {
                validator.PropertyChanged += RaiseValidatorsChanged;
            }
            
            OnPropertyChanged(nameof(Validators)); // Notify that the collection has changed
        }
        else if (args is { Action: NotifyCollectionChangedAction.Remove, OldItems: not null })
        {
            foreach (Validator validator in args.OldItems)
            {
                validator.PropertyChanged -= RaiseValidatorsChanged;
            }
            
            OnPropertyChanged(nameof(Validators));
        }
    }
    
    private void RaiseValidatorsChanged(object? sender, PropertyChangedEventArgs args)
    {
        OnPropertyChanged(nameof(Validators));
    }
}

/// <summary>
/// Generic version of the field model with a value of type T.
/// </summary>
/// <typeparam name="T">The type of the field value.</typeparam>
public abstract class Field<T> : Field
{
    /// <summary>
    /// The value of the field.
    /// </summary>
    private T? _value;
    public T? Value
    {
        get => _value;
        set => SetField(ref _value, value);
    }
}
