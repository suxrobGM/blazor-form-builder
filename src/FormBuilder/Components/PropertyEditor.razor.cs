using FormBuilder.Models;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

public partial class PropertyEditor : ComponentBase
{
    [Parameter]
    public Field? SelectedField { get; set; }
}
