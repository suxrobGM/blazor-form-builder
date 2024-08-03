namespace FormBuilder.Services;

/// <summary>
/// Facade service for form operations.
/// </summary>
public class FormService
{
    public FormService(IFormSerializer formSerializer, IFormApi formApi, ILovApi lovApi)
    {
        FormSerializer = formSerializer;
        FormApi = formApi;
        LovApi = lovApi;
    }
    
    /// <summary>
    /// Service for serializing and deserializing form designs.
    /// </summary>
    public IFormSerializer FormSerializer { get; }
    
    /// <summary>
    /// Represents the API for interacting with forms.
    /// </summary>
    public IFormApi FormApi { get; }
    
    /// <summary>
    /// Represents the API for interacting with list of values.
    /// </summary>
    public ILovApi LovApi { get; }
}
