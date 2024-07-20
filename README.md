# Blazor Form Builder
[![.NET Build](https://github.com/suxrobGM/blazor-form-builder/actions/workflows/deploy-gh-pages.yml/badge.svg)](https://github.com/suxrobGM/blazor-form-builder/actions/workflows/deploy-gh-pages.yml)

Blazor Form Builder is a comprehensive library for generating and managing forms within Blazor applications. 
It provides a simple interface for creating forms with various field types, including text, numeric, date, and dropdown fields. 
The library includes a Blazor WebAssembly application for designing and rendering forms from a JSON schema.

## Features

- **Dynamic Form Creation**: Easily create forms with text, numeric, date, and dropdown fields.
- **Form Designer**: A Blazor WebAssembly application for visually designing forms.
- **Form Renderer**: Render forms based on a JSON schema.
- **Integration**: Seamlessly integrates with a Web API for form data management.

## Getting Started

Follow these steps to set up and run the Blazor Form Builder:

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/) database installed and running on your machine

### Setup Instructions

1. **Clone the Repository**

   Clone this repository using the following command:
   ```sh
   git clone https://github.com/suxrobGM/blazor-form-builder.git
   cd blazor-form-builder
   ```

2. **Configure the Database**

   Update the connection string in the `appsettings.json` file located at `src/FormBuilder.API/appsettings.json` to match your PostgreSQL database configuration.

3. **Apply Database Migrations**

   Apply the database migrations by running the following command script:
   ```sh
   src/FormBuilder.API/Scripts/apply-migrations.cmd
   ```

4. **Run the Web API Project**

   Start the Web API project by running the following command script:
   ```sh
   src/scripts/run-api.cmd
   ```

5. **Run the Blazor Application**

   Start the Blazor WebAssembly application by running the following command script:
   ```sh
   src/scripts/run-app.cmd
   ```

### Accessing the Applications

Once the projects are running, you can access them via the following URLs:

- **Web API**: [https://localhost:8000/swagger/index.html](https://localhost:8000/swagger/index.html)
- **Blazor WASM App**: [https://localhost:8001](https://localhost:8001)

## Project Structure

- **FormBuilder**: The Razor class library containing reusable Blazor components for the form builder.
- **FormBuilder.API**: The Web API project for managing form data and LOV (List of Values) data.
- **FormBuilder.DesignerApp**: The Blazor WebAssembly project for designing and rendering forms.
