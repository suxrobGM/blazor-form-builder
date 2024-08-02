using FormBuilder.Factories;
using FormBuilder.Models;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

public partial class FieldValidatorsEditor : ComponentBase
{
    #region Parameters

    [Parameter, EditorRequired]
    public Field Field { get; set; } = default!;

    #endregion
    
    private void AddValidator(ValidatorType validatorType)
    {
        var validator = ValidatorFactory.Create(validatorType);
        Field.Validators.Add(validator);
    }
}
