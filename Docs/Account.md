# Api Specification for Account

## Register

Request:
- Method: POST
- Endpoint: `/api/account/register`
- Header:
  - Accept: application/json
  - Content-Type: application/json 

Body:

```json
{
  "name": "string",
  "username": "string|unique",
  "password": "string",
  "password_confirmation": "string"
}
```

Response:

```json
{
  "id": "string",
  "name": "string",
  "username": "string",
  "createdAt": "datetime",
  "updatedAt": "datetime"
}
```

## Login

Request:
- Method: POST
- Endpoint: `/api/account/login`
- Header:
  - Accept: application/json
  - Content-Type: application/json

Body:

```json
{
  "username": "string",
  "password": "string"
}
```

Response:

```json
{
  "accessToken": "string"
}
```

## Get Account

Request:
- Method: GET
- Endpoint: `/api/account/current`
- Header:
  - Authorization: Bearer <token> 
  - Accept: application/json

Response:

```json
{
  "id": "string",
  "name": "string",
  "username": "string",
  "createdAt": "datetime",
  "updatedAt": "datetime"
}
```