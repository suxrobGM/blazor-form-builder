using FormBuilder.Models;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

public partial class NumericField : ComponentBase
{
    [Parameter]
    public Field? Field { get; set; }
}
