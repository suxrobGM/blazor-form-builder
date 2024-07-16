using FormBuilder.Models;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

public partial class DateField : ComponentBase
{
    [Parameter]
    public Field? Field { get; set; }
}
