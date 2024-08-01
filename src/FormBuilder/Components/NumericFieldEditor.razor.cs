using FormBuilder.Models;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

public partial class NumericFieldEditor<TValue> : ComponentBase where TValue : struct
{
    [Parameter, EditorRequired]
    public NumericField<TValue> Field { get; set; } = default!;

    [Parameter]
    public EventCallback<NumericField<TValue>> FieldChanged { get; set; }

    private void OnStepChanged(string value)
    {
        Field.Step = value;
        FieldChanged.InvokeAsync(Field);
    }

    private void OnShowUpDownChanged(bool value)
    {
        Field.ShowUpDown = value;
        FieldChanged.InvokeAsync(Field);
    }

    private void OnFormatChanged(string? value)
    {
        Field.Format = value;
        FieldChanged.InvokeAsync(Field);
    }
}
