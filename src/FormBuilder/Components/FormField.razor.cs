using FormBuilder.Models;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

public partial class FormField : ComponentBase
{
    [Parameter, EditorRequired]
    public Field Field { get; set; } = default!;
}
