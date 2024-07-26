# How to integrate FormBuilder with your Blazor application

This guide explains how to integrate the Blazor Form Builder with your existing Blazor application. The integration process involves the following steps:

1. **Add the FormBuilder Razor class library to your Blazor application**:
    Add the `FormBuilder` Razor class library to your Blazor application as a project reference.
 
2. **Register the FormBuilder services**:
    Add `AddFormBuilder()` to the service collection in the `Program.cs` file. 
    Also, you need to pass `FormApiUrl` to the `AddFormBuilder()` method. The `FormApiUrl` is the URL of the Form Builder API.
    For example:
    ```csharp
    builder.Services.AddFormBuilder(options =>
    {
        options.FormApiUrl = "https://localhost:8000";
    });
    ```
   
3. **Add styles and scripts**:
    Include the required styles and scripts in the `index.html` file.
    Add the following styles to the `head` section:
    ```html
    <link href="_content/FormBuilder/css/app.css" rel="stylesheet" />
    ```
    The `app.css` file includes styles for Radzen components and Bootstrap 5.
    Alternatively, you can directly include Radzen styles and Bootstrap in your application.
    For example:
    ```html
    <link href="_content/Radzen.Blazor/css/standard-base.css" rel="stylesheet" /> <!-- Radzen standard theme -->
    <link href="_content/{YOUR_APPLICATION_NAME}/css/bootstrap/bootstrap.min.css" rel="stylesheet" />
    ```
    Add the following scripts to the `body` section:
    ```html
    <script src="_content/FormBuilder/js/app.js"></script>
    ```
    The `app.js` file includes scripts for Radzen components.
    Alternatively, you can directly include Radzen scripts in your application.
    For example:
    ```html
    <script src="_content/Radzen.Blazor/Radzen.Blazor.js"></script>
    ```
4. **Use FormEditor component**:
    Use the `FormEditor` component in your Blazor application to design forms.
    For example:
    ```html
    <FormEditor />
    ```
    
5. **Use FormRenderer component**:
    Use the `FormRenderer` component in your Blazor application to render forms.
    For example:
    ```html
    <FormRenderer FormId="1" />
    ```

You can view a sample Blazor application that integrates the Blazor Form Builder in the `src/FormBuilder.DesignerApp` project. 
The sample application demonstrates how to design and render forms using the Blazor Form Builder components.

