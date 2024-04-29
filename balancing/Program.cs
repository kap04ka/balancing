using balancing.Models;
using balancing.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ICalculatorService, CalculatorService>();

var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
var user = Environment.GetEnvironmentVariable("DB_USER") ?? "postgres";
var database = Environment.GetEnvironmentVariable("DB_NAME") ?? "balance";
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "admin";

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseNpgsql($"Host={host}; Port={port}; Database={database}; Username={user}; Password={dbPassword}"));

var app = builder.Build();

app.UseCors(builder => builder
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseAuthorization();

app.MapControllers();

app.Run();
