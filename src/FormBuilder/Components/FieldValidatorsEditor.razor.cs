using FormBuilder.Factories;
using FormBuilder.Models;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

public partial class FieldValidatorsEditor : ComponentBase
{
    #region Parameters

    [Parameter, EditorRequired]
    public Field Field { get; set; } = default!;
    
    [Parameter]
    public EventCallback<Field> FieldChanged { get; set; }

    #endregion
    
    private void RaiseFieldChanged()
    {
        FieldChanged.InvokeAsync(Field);
    }
    
    private void AddValidator(ValidatorType validatorType)
    {
        Field.Validators.Add(ValidatorFactory.Create(validatorType));
        FieldChanged.InvokeAsync(Field);
    }
}
