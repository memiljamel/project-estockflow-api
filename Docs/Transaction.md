# Api Specification for Transaction

## Get Transactions

Request:
- Method: GET
- Endpoint: `/api/pos/transactions`
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
    "item": "string",
    "quantity": "int",
    "category": "enum",
    "price": "decimal",
    "amount": "decimal",
    "createdAt": "datetime",
    "updatedAt": "datetime"
  }
]
```

## Create Transaction

Request:
- Method: POST
- Endpoint: `/api/pos/transactions`
- Header:
  - Authorization: Bearer <token>
  - Accept: application/json
  - Content-Type: application/json

Body:

```json
{
  "item": "string|exists",
  "quantity": "int",
  "category": "enum",
  "price": "decimal",
  "amount": "decimal"
}
```

Response:

```json
{
  "id": "int",
  "item": "string",
  "quantity": "int",
  "category": "enum",
  "price": "decimal",
  "amount": "decimal",
  "createdAt": "datetime",
  "updatedAt": "datetime"
}
```