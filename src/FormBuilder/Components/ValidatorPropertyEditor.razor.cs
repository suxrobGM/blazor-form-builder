using FormBuilder.Models;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

public partial class ValidatorPropertyEditor<TValue> : ComponentBase where TValue : Validator
{
    #region Parameters

    [Parameter, EditorRequired]
    public TValue Validator { get; set; } = default!;
    
    [Parameter]
    public EventCallback<TValue> ValidatorChanged { get; set; }

    #endregion
    
    private void OnTextChanged(string value)
    {
        Validator.Text = value;
        ValidatorChanged.InvokeAsync(Validator);
    }
    
    private void OnShowAsPopupChanged(bool value)
    {
        Validator.ShowAsPopup = value;
        ValidatorChanged.InvokeAsync(Validator);
    }
    
    private void OnShowRequiredHintChanged(bool value)
    {
        if (Validator is RequiredValidator requiredValidator)
        {
            requiredValidator.ShowRequiredHint = value;
            ValidatorChanged.InvokeAsync(Validator);
        }
    }
    
    private void OnMinLengthChanged(int? value)
    {
        if (Validator is LengthValidator lengthValidator)
        {
            lengthValidator.MinLength = value;
            ValidatorChanged.InvokeAsync(Validator);
        }
    }
    
    private void OnMaxLengthChanged(int? value)
    {
        if (Validator is LengthValidator lengthValidator)
        {
            lengthValidator.MaxLength = value;
            ValidatorChanged.InvokeAsync(Validator);
        }
    }
    
    private void OnMinChanged(int value)
    {
        if (Validator is NumericRangeValidator numericRangeValidator)
        {
            numericRangeValidator.Min = value;
            ValidatorChanged.InvokeAsync(Validator);
        }
    }
    
    private void OnMaxChanged(int value)
    {
        if (Validator is NumericRangeValidator numericRangeValidator)
        {
            numericRangeValidator.Max = value;
            ValidatorChanged.InvokeAsync(Validator);
        }
    }
}
