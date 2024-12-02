# Api Specification for Stock

## Get Stocks

Request:
- Method: GET
- Endpoint: `/api/items/stock`
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
    "createdAt": "datetime",
    "updatedAt": "datetime"
  }
]
```