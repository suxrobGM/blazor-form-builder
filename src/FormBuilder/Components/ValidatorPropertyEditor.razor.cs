using FormBuilder.Models;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

public partial class ValidatorPropertyEditor<TValue> : ComponentBase where TValue : Validator
{
    #region Parameters

    [Parameter, EditorRequired]
    public TValue Validator { get; set; } = default!;
    
    [Parameter]
    public RenderFragment? Header { get; set; }

    #endregion
}
