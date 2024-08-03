# How to Integrate FormBuilder with Your Blazor Application

This guide explains how to integrate the Blazor FormBuilder with your existing Blazor application. 
The integration process involves the following steps:

## Step 1: Add the FormBuilder Razor Class Library

Add the `FormBuilder` Razor class library to your Blazor application as a project reference.

## Step 2: Register the FormBuilder Services

Add `AddFormBuilder()` to the service collection in the `Program.cs` file. 
You also need to pass the `FormApiHost` to the `AddFormBuilder()` method. 
The `FormApiHost` is the base URL of the Form Builder API.

Example:

```csharp
builder.Services.AddFormBuilder(options =>
{
    options.FormApiHost = "https://localhost:8000";
});
```

You can also set optional parameters such as `Theme` and `CacheExpiration`:
- `Theme`: The theme to use for the Radzen components. The default value is `material`. 
You can view the [Themes](../src/FormBuilder/Themes.cs) class for available themes.
- `CacheExpiration`: The expiration time for the form cache. The default value is `30 minutes`.
By default, the LOV (list of values) API requests are cached.

Example:
```csharp
builder.Services.AddFormBuilder(options =>
{
    options.FormApiHost = "https://localhost:8000";
    options.Theme = Themes.Standard;
    options.CacheExpiration = TimeSpan.FromMinutes(15);
});
```

## Step 3: Add `<InitComponents />`

Add the `<InitComponents />` component to the layout file to initialize the Radzen components and load static files.

For example, add the following code to the `MainLayout.razor` file:

```html
<InitComponents />
```

And don't forget to include namespace in the `_Imports.razor` file:

```csharp
@using FormBuilder
@using FormBuilder.Components
```

That's it! You have successfully integrated the Blazor FormBuilder with your Blazor application.

## Usage FormEditor Component
The `FormEditor` component is used to design forms. 
To use the `FormEditor` component, add the following code to the desired page:

```html
<FormEditor />
```

## Usage FormRenderer Component
The `FormRenderer` component is used to render forms.
To use the `FormRenderer` component, add the following code to the desired page:

```html
<FormRenderer FormId="1" />
```

To read more about the `FormRenderer` component, refer to the [Form Renderer Component Reference](form-renderer.md).

## Sample Application

You can view a sample Blazor application that integrates the Blazor Form Builder components in the `src/FormBuilder.DesignerApp` project. 
The sample application demonstrates how to design and render forms using the Blazor FormBuilder components.
