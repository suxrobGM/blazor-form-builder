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
        if (HasValidator(validatorType))
        {
            return;
        }
        
        var validator = ValidatorFactory.Create(validatorType);
        Field.Validators.Add(validator);
    }
    
    private void RemoveValidator(Validator validator)
    {
        Field.Validators.Remove(validator);
    }
    
    private bool HasValidator(ValidatorType validatorType)
    {
        return Field.Validators.Any(v => v.Type == validatorType);
    }
}
