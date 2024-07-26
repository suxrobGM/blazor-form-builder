# Form Builder REST API Reference

The Form Builder REST API provides endpoints to manage forms and list of values (LOV).
You can use the Form Builder API to create, update, delete, and retrieve forms and LOV data.

## Form API
- `GET /api/forms`: Get all forms in the form of paged response.
    - Query Parameters:
        - `page`: The non-zero-based page number. Default is 1.
        - `pageSize`: The number of items per page. Default is 10.
    - Response:
        - `pageSize`: The number of items per page.
        - `pagesCount`: The total number of available pages.
        - `data`: An array of form data.
