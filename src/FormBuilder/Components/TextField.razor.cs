using FormBuilder.Models;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

public partial class TextField : ComponentBase
{
    [Parameter]
    public Field? Field { get; set; }
}
