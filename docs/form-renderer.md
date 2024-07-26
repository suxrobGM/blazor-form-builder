# Form Renderer Component Reference

The Form Renderer component is used to render forms in that designed by the `FormEditor` component.
You can specify either the `FormId` or `FormJson` property to render a form.

## FormRenderer Properties
- `FormId`: The ID of the form to render. It fetches form data from the API based on the ID.
- `FormJson`: The JSON data of the form to render. You can pass the JSON data of the form directly to the component.
- `IsLoadingForm`: A boolean value that indicates whether the form is loading and rendering.

## FormRenderer Events
- `IsLoadingFormChanged`: An event that is triggered when the `IsLoadingForm` property changes. You can use this event to perform additional actions when the form is loading.
- `FormLoaded`: An event that is triggered when the form is loaded and rendered. You can use this event to perform additional actions when the form is loaded.

## FormRenderer public methods
- `LoadFormFromIdAsync(string formId)`: A method that loads the form data from the API based on the form ID.
- `LoadFormFromJsonAsync(string formJson)`: A method that loads the form data from the JSON data.
- `GetFormAsJsonAsync()`: A method that returns the JSON data of the rendered form.

## Examples
## Render a form using FormId or FormJson properties
```html
<FormRenderer FormId="1" />
```
```html
<FormRenderer FormJson="@formJson" />
```

## Render a form dynamically using methods
```html
<FormRenderer @ref="formRendererRef" />

<RadzenTextBox @bind-Value="_formId" />
<RadzenButton Text="Load Form" Click="@LoadForm" />
```
```csharp
@code {
    private FormRenderer? formRendererRef;
    private string? _formId;
    
    private async Task LoadForm()
    {
        if (formRendererRef is null || string.IsNullOrEmpty(_formId))
        {
            return;
        }
        
        await formRenderer.LoadFormFromIdAsync(_formId);
    }
}
```
