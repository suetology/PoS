using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Tax;
using PoS.WebApi.Application.Services.Business;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Persistence;
using PoS.WebApi.Infrastructure.Repositories;

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

// Registering dependencies
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<IBusinessService, BusinessService>();
builder.Services.AddTransient<IBusinessRepository, BusinessRepository>();

builder.Services.AddTransient<ITaxService, TaxService>();

builder.Services.AddTransient<ITaxRepository, TaxRepository>();

// Adding controllers
builder.Services.AddControllers();

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

// Mapping Controllers
app.MapControllers();

// Running app
app.Run();