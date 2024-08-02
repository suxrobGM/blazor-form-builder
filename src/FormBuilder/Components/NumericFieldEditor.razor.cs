using System.Globalization;
using FormBuilder.Models;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

public partial class NumericFieldEditor<TValue> : ComponentBase where TValue : struct
{
    #region Parameters

    [Parameter, EditorRequired]
    public NumericField<TValue> Field { get; set; } = default!;
    
    /// <summary>
    /// Event that is triggered when a field property changes such as step, format, etc.
    /// </summary>
    [Parameter]
    public EventCallback<FieldTypeChangedEventArgs> PropertyChanged { get; set; }

    #endregion
    

    #region Binding Properties

    private decimal _step;
    private decimal Step
    {
        get => _step;
        set
        {
            if (_step == value)
            {
                return;
            }

            _step = value;
            Field.Step = value.ToString(CultureInfo.InvariantCulture);
        }
    }

    #endregion

    protected override void OnInitialized()
    {
        if (decimal.TryParse(Field.Step, out var step))
        {
            _step = step;
        }
    }
}
