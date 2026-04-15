using RefitKeycloak.Application;
using RefitKeycloak.Application.Interfaces;
using RefitKeycloak.Api.Extensions;
using RefitKeycloak.Infrastructure;
using RefitKeycloak.Infrastructure.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddKeycloakAuthentication(builder.Configuration);
builder.Services.AddSwaggerDocumentation();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/health", () => Results.Ok("Refit-Keycloak API online"));

app.MapGet("/posts", async (IPostService service) =>
{
    var result = await service.GetPosts();
    return Results.Ok(result);
});

app.MapGet("/secure", () => "ok")
    .RequireAuthorization();

app.Run();
