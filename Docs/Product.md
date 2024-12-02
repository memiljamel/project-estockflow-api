# Api Specification for Product

## Get Products

Request:
- Method: GET
- Endpoint: `/api/items`
- Header:
  - Authorization: Bearer <token>
  - Accept: application/json
- Query:
  - name: null
  - price: null
  - stock: null
  - category: null
  - page: 1
  - size: 15

Response:

```json
[
  {
    "id": "int",
    "name": "string",
    "price": "decimal",
    "stock": "int",
    "category": "enum",
    "imageUrl": "string",
    "createdAt": "datetime",
    "updatedAt": "datetime"
  }
]
```

## Create Product

Request:
- Method: POST
- Endpoint: `/api/items`
- Header:
  - Authorization: Bearer <token>
  - Accept: application/json
  - Content-Type: multipart/form-data 

Body:

```json
{
  "name": "string",
  "price": "decimal",
  "stock": "int",
  "category": "enum",
  "image": "file"
}
```

Response:

```json
{
  "id": "int",
  "name": "string",
  "price": "decimal",
  "stock": "int",
  "category": "enum",
  "imageUrl": "string",
  "createdAt": "datetime",
  "updatedAt": "datetime"
}
```

## Update Product

Request:
- Method: PUT
- Endpoint: `/api/items/{itemId}`
- Header:
  - Authorization: Bearer <token>
  - Accept: application/json
  - Content-Type: multipart/form-data

Body:

```json
{
  "name": "string",
  "price": "decimal",
  "stock": "int",
  "category": "enum",
  "image": "file"
}
```

Response:

```json
{
  "id": "int",
  "name": "string",
  "price": "decimal",
  "stock": "int",
  "category": "enum",
  "imageUrl": "string",
  "createdAt": "datetime",
  "updatedAt": "datetime"
}
```

## Delete Product

Request:
- Method: DELETE
- Endpoint: `/api/items/{itemId}`
- Header:
  - Authorization: Bearer <token>
  - Accept: application/json

Response: 

204 (No Content)
