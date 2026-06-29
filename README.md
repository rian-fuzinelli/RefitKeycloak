# Refit + Keycloak with .NET 10

A .NET 10 API with layered architecture, JWT authentication via Keycloak, and external HTTP integration via Refit

## Architecture

- `Refit-Keycloak.Domain`: domain entities and business rules
- `Refit-Keycloak.Application`: use cases and application services
- `Refit-Keycloak.Infrastructure`: external integrations (Refit and Keycloak)
- `Refit-Keycloak.Api`: HTTP entry layer (Minimal API + Swagger)
- `Refit-Keycloak.Tests`: unit tests

## Tech stack

- .NET 10
- ASP.NET Core Minimal API
- Refit
- Keycloak (JWT Bearer)
- Swagger/OpenAPI (Swashbuckle)
- xUnit + Moq

## Prerequisites

- .NET 10 SDK installed
- Docker and Docker Compose installed

## Startup guide

### 1) Start Keycloak

```bash
docker-compose up -d
```

Open `http://localhost:8080` and sign in with:

- user: `admin`
- password: `admin`

### 2) Configure realm and client in Keycloak

1. Create realm `dev`
2. Create client `api-client`
3. To test tokens easily, configure the client as:
   - Access Type: `Confidential`
   - Service Accounts Enabled: `ON`
4. Copy the `Client secret`

Note: the API expects the following values in `Refit-Keycloak.Api/appsettings.json`:

- Authority: `http://localhost:8080/realms/dev`
- Audience: `api-client`

### 3) Restore packages

```bash
dotnet restore RefitKeycloak.slnx
```

### 4) Build the sln

```bash
dotnet build RefitKeycloak.slnx
```

### 5) Run the API

```bash
dotnet run --project Refit-Keycloak.Api
```

By default (launch profile), the API runs on `http://localhost:5201`.

## Endpoints

- `GET /` -> Swagger UI
- `GET /swagger/v1/swagger.json` -> OpenAPI document
- `GET /health` -> simple health check
- `GET /posts` -> public endpoint (external data via Refit)
- `GET /secure` -> JWT-protected endpoint (Keycloak)

## Test the protected endpoint (`/secure`)

### 1) Get a token from Keycloak (client credentials)

Example with `curl`:

```bash
curl -X POST "http://localhost:8080/realms/dev/protocol/openid-connect/token" \
  -H "Content-Type: application/x-www-form-urlencoded" \
  -d "grant_type=client_credentials" \
  -d "client_id=api-client" \
  -d "client_secret=YOUR_CLIENT_SECRET"
```

Copy the returned `access_token`.

### 2) Call the protected endpoint

```bash
curl -H "Authorization: Bearer YOUR_ACCESS_TOKEN" http://localhost:5201/secure
```

## Run unit tests

```bash
dotnet test RefitKeycloak.slnx
```

## Quick troubleshooting

- Port already in use: stop the previous process running on port `5201`
- `401` on `/secure`: validate `Authority`, `Audience`, `client_id`, and token
- Swagger not opening: ensure the API is running and access `http://localhost:5201/`
