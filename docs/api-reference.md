# Form Builder REST API Reference

The Form Builder REST API provides endpoints to manage forms and list of values (LOV).
You can use the Form Builder API to create, update, delete, and retrieve forms and LOV data.

All endpoints have general response structure, with `success`, `data`, and `error` fields.
The `success` field indicates whether the request was successful.
If the request was successful, the `data` field contains the response data. 
Otherwise, the `error` field contains the error message and the `data` field is not present.

```json
{
  "success": true,
  "error": "Error message", // Only if success is false
  "data": { // Only if success is true
    "id": 1,
    "formName": "Form 1",
    "formDesign": "{JSON Data}"
  }
}
```

For pagination, the response contains `pageSize`, `pagesCount`, and `data` fields.
```json
{
  "success": true,
  "pageSize": 10, // Number of items per page
  "pagesCount": 1, // Total number of pages
  "data": [
    {
      "id": 1,
      "formName": "Form 1",
      "formDesign": "{JSON Data}"
    }
  ]
}
```

#### GET `/api/forms`
##### Summary:

Gets a paged list of forms.

##### Parameters

| Name     | Located in | Description                                  | Required | Schema  |
|----------|------------|----------------------------------------------|----------|---------|
| OrderBy  | query      | The field to order by.                       | No       | string  |
| Page     | query      | Non-zero-based page number. Default is 1.    | No       | integer |
| PageSize | query      | The number of items per page. Default is 10. | No       | integer |

##### Responses

| Code | Description |
|------|-------------|
| 200  | Success     |

Sample request,
```
GET /api/forms?OrderBy=formName&Page=1&PageSize=10
```

Response body example,
```json
{
  "success": true,
  "pageSize": 10,
  "pagesCount": 1,
  "data": [
    {
      "id": 1,
      "formName": "Form 1",
      "formDesign": "{JSON Data}"
    },
    {
      "id": 2,
      "formName": "Form 2",
      "formDesign": "{JSON Data}"
    }
  ]
}
```

### POST `/api/forms`
##### Summary:

Creates a new form.

##### Responses

| Code | Description |
|------|-------------|
| 200  | Success     |
| 400  | Bad Request |

Success response body example,
```json
{
  "success": true,
  "data": {
    "id": 1, // Returned form ID
    "formName": "Form 1",
    "formDesign": "{JSON Data}"
  }
}
```

Error response body example,
```json
{
  "success": false,
  "error": "Error message"
}
```

### GET `/api/forms/{id}`
##### Summary:

Gets a form by its id.

##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path | Form ID | Yes | string |

##### Responses

| Code | Description |
|------|-------------|
| 200  | Success     |
| 400  | Bad Request |

### PUT `/api/forms/{id}`
##### Summary:

Updates a form by its id.

##### Parameters

| Name | Located in | Description      | Required | Schema |
|------|------------|------------------|----------|--------|
| id   | path       | Existing form ID | Yes      | string |

Sample request,
```
PUT /api/forms/1
```

Example request body,
```json
{
  "formName": "Form 1",
  "formDesign": "{JSON Data}"
}
```

##### Responses

| Code | Description |
|------|-------------|
| 200  | Success     |
| 400  | Bad Request |

### DELETE `/api/forms/{id}`
##### Summary:

Deletes a form by its id.

##### Parameters

| Name | Located in | Description | Required | Schema |
|------|------------|-------------|----------|--------|
| id   | path       | Form ID     | Yes      | string |

##### Responses

| Code | Description |
|------|-------------|
| 200  | Success     |
| 400  | Bad Request |


### GET `/api/lov`
##### Summary:

Gets a paged list of List IDs.

##### Parameters

| Name     | Located in | Description                                  | Required | Schema  |
|----------|------------|----------------------------------------------|----------|---------|
| OrderBy  | query      | The field to order by.                       | No       | string  |
| Page     | query      | Non-zero-based page number. Default is 1.    | No       | integer |
| PageSize | query      | The number of items per page. Default is 10. | No       | integer |

##### Responses

| Code | Description |
|------|-------------|
| 200  | Success     |

Sample request,
```
GET /api/lov?Page=1&PageSize=10
```

Response body example,
```json
{
  "success": true,
  "pageSize": 10,
  "pagesCount": 1,
  "data": [
    {
      "id": 1,
      "listId": 1,
      "listValue": "Value 1"
    },
    {
      "id": 2,
      "listId": 2,
      "listValue": "Value 2"
    }
  ]
}
```

### POST `/api/lov`
##### Summary:

Batch add list of values

##### Responses

| Code | Description |
|------|-------------|
| 200  | Success     |

Request body example,
```json
{
  "lovs": [
    {
      "listId": 1,
      "listValue": "Value 1"
    },
    {
      "listId": 2,
      "listValue": "Value 2"
    }
  ]
}
```

### GET `/api/lov/{listId}`
##### Summary:

Gets list of values filtered by ListId

##### Parameters

| Name   | Located in | Description | Required | Schema  |
|--------|------------|-------------|----------|---------|
| listId | path       |             | Yes      | integer |

##### Responses

| Code | Description |
|------|-------------|
| 200  | Success     |

Sample request,
```
GET /api/lov/1
```

Success response body example,
```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "listId": 1,
      "listValue": "Value 1"
    },
    {
      "id": 2,
      "listId": 1,
      "listValue": "Value 2"
    },
    {
      "id": 3,
      "listId": 1,
      "listValue": "Value 3"
    }
  ]
}
```

### POST `/api/lov/delete`
##### Summary:

Batch delete list of values

##### Responses

| Code | Description |
|------|-------------|
| 200  | Success     |

Request body example,
```json
{
  "listIds": [1, 2, 3]
}
```
