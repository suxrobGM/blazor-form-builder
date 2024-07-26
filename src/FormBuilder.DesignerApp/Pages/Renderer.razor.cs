using FormBuilder.Components;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.DesignerApp.Pages;

public partial class Renderer : ComponentBase
{
    private FormRenderer? _formRendererRef;
    private InputModel _input = new();
    private bool _isLoading;
    
    #region Input Model

    private class InputModel
    {
        public string? FormId { get; set; }
        public string? FormJson { get; set; }
    }

    #endregion
    
    
    private Task LoadFormAsync()
    {
        if (_formRendererRef is null)
        {
            return Task.CompletedTask;
        }
        
        if (!string.IsNullOrEmpty(_input.FormId))
        {
            return _formRendererRef.LoadFormFromIdAsync(_input.FormId);
        }
        
        if (!string.IsNullOrEmpty(_input.FormJson))
        {
            return _formRendererRef.LoadFormFromJsonAsync(_input.FormJson);
        }
        
        return Task.CompletedTask;
    }
}
