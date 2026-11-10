using DepositoParis.Api.Middleware;
using DepositoParis.Application;
using DepositoParis.Infrastructure;
using DepositoParis.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Capas
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Permite que el frontend de Angular consuma la API
const string PoliticaFront = "Frontend";
builder.Services.AddCors(options => options.AddPolicy(PoliticaFront, policy =>
    policy.WithOrigins(builder.Configuration.GetSection("Cors:Origenes").Get<string[]>() ?? ["http://localhost:4200"])
          .AllowAnyHeader()
          .AllowAnyMethod()));

var app = builder.Build();

// Crea la base de datos SQLite y carga los datos iniciales si no existen
app.Services.GetRequiredService<DatabaseInitializer>().Inicializar();

app.UseMiddleware<ManejoErroresMiddleware>();

app.MapOpenApi();
app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Depósito París API"));

app.UseCors(PoliticaFront);
app.MapControllers();

app.Run();
