# Form Renderer Component Reference

The Form Renderer component is used to render forms designed by the `FormEditor` component. 
You can specify either the `FormId` or `FormJson` property to render a form.

## FormRenderer Properties

- **`FormId`**: The ID of the form to render. This fetches form data from the API based on the ID.
- **`FormJson`**: The JSON data of the form to render. This allows you to pass the JSON data of the form directly to the component.
- **`IsLoadingForm`**: A boolean value indicating whether the form is currently loading and rendering.

## FormRenderer Events

- **`IsLoadingFormChanged`**: An event triggered when the `IsLoadingForm` property changes. Use this event to perform additional actions when the form is loading.
- **`FormLoaded`**: An event triggered when the form is fully loaded and rendered. Use this event to perform additional actions when the form is loaded.

## FormRenderer Public Methods

- **`LoadFormFromIdAsync(string formId)`**: Loads the form data from the API based on the form ID.
- **`LoadFormFromJsonAsync(string formJson)`**: Loads the form data from the provided JSON data.
- **`GetFormAsJsonAsync()`**: Returns the JSON data of the rendered form.

## Examples

### Render a form using FormId or FormJson properties

Render a form using the `FormId` property:

```html
<FormRenderer FormId="1" />
```

Render a form using the `FormJson` property:

```html
<FormRenderer FormJson="@formJson" />
```

### Render a form dynamically using methods

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

        await formRendererRef.LoadFormFromIdAsync(_formId);
    }
}
```

In these examples, the `FormRenderer` component can be used to dynamically load forms based on either a form ID or JSON data. 
The component also provides events and methods to manage the loading state and retrieve the rendered form data as JSON.
