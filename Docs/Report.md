# Api Specification for Report

## Get Reports

Request:
- Method: GET
- Endpoint: `/api/pos/reports`
- Header:
  - Authorization: Bearer <token>
  - Accept: application/json
- Query:
  - total: null
  - name: null
  - price: null
  - stock: null
  - category: null
  - quantity: null
  - amount: null
  - page: 1
  - size: 15

Response:

```json
{
  "total": "int",
  "transactions": [
    {
      "id": "string",
      "item": {
        "id": "string",
        "name": "string",
        "price": "decimal",
        "stock": "int",
        "category": "enum",
        "imageUrl": "string"
      },
      "quantity": "int",
      "amount": "decimal",
      "createdAt": "datetime",
      "updatedAt": "datetime"
    }
  ]
}
```