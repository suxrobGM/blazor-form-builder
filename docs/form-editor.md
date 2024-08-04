# Form Editor Component Reference

The Form Editor component is a tool for designing dynamic forms. With this component, you can add, edit, and delete form elements interactively. 
It provides a user-friendly interface for building forms, including support for drag-and-drop functionality. 
In addition, the Form Editor allows you to configure form element properties and validators.

## User Interface Overview

The Form Editor component offers an intuitive UI for form design:

![Form Editor](./img/form-editor-1.jpg?raw=true)

It consists of the following main areas:
- **Fields**: The area where you can add form elements by dragging and dropping or by clicking the `Add Field` button.
- **Property Editor**: The UI on the right side of the editor where you can configure the properties of the selected form element.
- **Generated JSON**: The JSON representation of the designed form.

### Supported Form Elements

You can add the following form elements to your forms:

- **TextField**: Corresponds to the `RadzenTextBox` component.
- **NumericIntField**: Corresponds to the `RadzenNumeric` component (integer values).
- **NumericDecimalField**: Corresponds to the `RadzenNumeric` component (decimal values).
- **DateField**: Corresponds to the `RadzenDatePicker` component.
- **SelectField**: Corresponds to the `RadzenDropDown` component.

These elements can be added by dragging and dropping them from the toolbox to the `Fields` area in the editor.
Also, you can rearrange the form elements by dragging them within the `Fields` area.

## Property Editor UI

When a form element is selected in the `Fields` area, the `Property Editor` UI appears on the right side of the Form Editor. This UI allows you to configure the properties of the selected form element.

![Property Editor](./img/form-editor-2.jpg?raw=true)

### Common Properties for All Fields

- **ID**: The unique identifier for the form element. This property is disabled for editing.
- **Type**: The type of the form element.
- **Label**: The label displayed alongside the form element.
- **Placeholder**: The placeholder text within the form element.
- **Hint**: The hint text displayed below the form element.
- **Read Only**: A boolean value indicating whether the form element is read-only.
- **Disabled**: A boolean value indicating whether the form element is disabled and cannot be interacted with.

### Additional Properties by Field Type

#### Numeric Fields (NumericIntField, NumericDecimalField)
- **Step**: The increment/decrement step value for the numeric field.
- **Show Up Down**: A boolean value indicating whether the up and down buttons are displayed for the numeric field.
- **Format**: The format string for the numeric field (e.g., `C` for currency). For more details on format strings, see the [Microsoft documentation](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings).

#### Date Field
- **Date Format**: The format string for the date field (e.g., `dd/MM/yyyy`). For more details on date format strings, see the [Microsoft documentation](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings).

#### Select Field
- **List ID**: The ID of the list of values (LOV) to use for the select field. This ID is used to fetch the list of values from the API. The component uses virtualization to load data on demand.
- **List Values**: The list of values to display in the select field when the `List ID` is specified.

## Field Validators UI

The `Property Editor` UI includes a `Field Validators` section, where you can add, edit, and delete form field validators.

![Field Validators](./img/form-editor-3.jpg?raw=true)

### Available Validators

- **Required Validator**: Ensures the form field is not left empty. Applies to all form elements.
    - **Show Required Hint**: Indicates whether the required hint is displayed below the form field when it is empty.
- **Email Validator**: Validates that the form field contains a valid email address. Applies to the `TextField`.
- **Length Validator**: Ensures the form field value meets the specified length requirements. Applies to the `TextField`.
    - **Min Length**: The minimum allowed length of the form field value.
    - **Max Length**: The maximum allowed length of the form field value.
- **Range Validator**: Ensures the form field value is within a specified numeric range. Applies to `NumericIntField` and `NumericDecimalField`.
    - **Min Value**: The minimum allowed value for the form field.
    - **Max Value**: The maximum allowed value for the form field.

### Common Properties for All Validators

- **Error Text**: The error message displayed when validation fails.
- **Show as Popup**: Indicates whether the error message is displayed as a popup instead of inline below the form field.

## Form Services Customization

The FormBuilder components (`FormEditor` and `FormRenderer`) rely on the following services:

- **IFormSerializer**: Handles serialization and deserialization of form data.
- **IFormApi**: Manages interactions with the Form REST API.
- **ILovApi**: Manages interactions with the List of Values (LOV) REST API.

These services come with default implementations that work out of the box. However, you can customize them by implementing your own services and registering them in the service collection.

To register your custom services, use the `AddScoped` method in the `Program.cs` file:

```csharp
builder.Services.AddScoped<IFormSerializer, CustomFormSerializer>();
builder.Services.AddScoped<IFormApi, CustomFormApi>();
builder.Services.AddScoped<ILovApi, CustomLovApi>();
```

## Load Form Dialog
To load an existing form into the Form Editor, click the `Load Form` button in the toolbar. 
This action opens a dialog where you can enter either the form ID or the JSON data of the form to load.
If you enter the form ID, the Form Editor fetches the form data from the API based on the ID.
