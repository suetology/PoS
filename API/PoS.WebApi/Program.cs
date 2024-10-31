using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Db setup
var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<DatabaseContext>(options => 
    options.UseSqlite(connectionString));

// Swagger setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Building app
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Adding middleware
app.UseHttpsRedirection();
app.UseCors("AllowAll");

// Running app
app.Run();