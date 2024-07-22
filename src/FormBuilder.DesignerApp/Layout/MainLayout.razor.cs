using Microsoft.AspNetCore.Components;

namespace FormBuilder.DesignerApp.Layout;

public partial class MainLayout : LayoutComponentBase
{
    private bool _sidebarExpanded = true;
    
    private void ToggleSidebar()
    {
        _sidebarExpanded = !_sidebarExpanded;
    }
}
