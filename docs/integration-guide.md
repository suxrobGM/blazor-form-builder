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

## Step 3: Add Styles, Scripts, and Required Components

Include the required styles and scripts in the `index.html` file.

### Add Styles

Add the following styles to the `head` section:

```html
<link href="_content/FormBuilder/css/app.css" rel="stylesheet" />
```

The `app.css` file includes styles for Radzen components and Bootstrap 5.

Alternatively, you can directly include Radzen styles and Bootstrap in your application:

```html
<link href="_content/Radzen.Blazor/css/standard-base.css" rel="stylesheet" /> <!-- Radzen standard theme -->
<link href="_content/{YOUR_APPLICATION_NAME}/css/bootstrap/bootstrap.min.css" rel="stylesheet" />
```

### Add Scripts

Add the following scripts to the `body` section:

```html
<script src="_content/FormBuilder/js/app.js"></script>
```

The `app.js` file includes scripts for Radzen components.

Alternatively, you can directly include Radzen scripts in your application:

```html
<script src="_content/Radzen.Blazor/Radzen.Blazor.js"></script>
```
### Add Required Components
Add the `RadzenComponents` component to the layout file.
It is used to render Radzen notifications and dialog components.
For example, add the following code to the `MainLayout.razor` file:

```html
<RadzenComponents />
```

And don't forget to include namespace in the `_Imports.razor` file:

```csharp
@using FormBuilder.Components
```

## Step 4: Use FormEditor Component

Use the `FormEditor` component in your Blazor application to design forms.

Example:

```html
<FormEditor />
```

## Step 5: Use FormRenderer Component

Use the `FormRenderer` component in your Blazor application to render forms.

Example:

```html
<FormRenderer FormId="1" />
```

## Sample Application

You can view a sample Blazor application that integrates the Blazor Form Builder components in the `src/FormBuilder.DesignerApp` project. 
The sample application demonstrates how to design and render forms using the Blazor FormBuilder components.
