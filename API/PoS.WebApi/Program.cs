using System.Net;
using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.ServiceCharge;
using PoS.WebApi.Application.Services.Customer;
using PoS.WebApi.Application.Services.Tax;
using PoS.WebApi.Application.Services.Business;
using PoS.WebApi.Application.Services.User;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Infrastructure.Persistence;
using PoS.WebApi.Infrastructure.Repositories;
using PoS.WebApi.Presentation.Extensions;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using PoS.WebApi.Application.Services.Auth;
using PoS.WebApi.Application.Services.Shift;
using PoS.WebApi.Application.Services.Service;
using PoS.WebApi.Application.Services.Reservation;
using PoS.WebApi.Infrastructure.Security;
using PoS.WebApi.Application.Services.Discount;
using PoS.WebApi.Application.Services.Order;
using PoS.WebApi.Application.Services.ItemGroup;
using PoS.WebApi.Application.Services.Item;
using PoS.WebApi.Application.Services.Payments;
using PoS.WebApi.Infrastructure.Payments.Extensions;
using PoS.WebApi.Application.Services.Notification;
using PoS.WebApi.Application.Services.Refund;
using PoS.WebApi.Infrastructure.Security.Exceptions;
using PoS.WebApi.Application.Services.Reservation.Exceptions;
using PoS.WebApi.Application.Services.Order.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Db setup
var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<DatabaseContext>(options => 
    options.UseSqlite(connectionString));

// Swagger setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Description = "Please enter a valid token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// CORS
builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });

    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Registering dependencies
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<IServiceChargeRepository, ServiceChargeRepository>();
builder.Services.AddTransient<IServiceChargeService, ServiceChargeService>();

builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<ICustomerService, CustomerService>();

builder.Services.AddTransient<IShiftService, ShiftService>();
builder.Services.AddTransient<IShiftRepository, ShiftRepository>();

builder.Services.AddTransient<ITaxService, TaxService>();
builder.Services.AddTransient<ITaxRepository, TaxRepository>();

builder.Services.AddTransient<IBusinessService, BusinessService>();
builder.Services.AddTransient<IBusinessRepository, BusinessRepository>();

builder.Services.AddTransient<IServiceRepository, ServiceRepository>();
builder.Services.AddTransient<IServiceService, ServiceService>();

builder.Services.AddTransient<IDiscountRepository, DiscountRepository>();
builder.Services.AddTransient<IDiscountService, DiscountService>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IOrderService, PoS.WebApi.Application.Services.Order.OrderService>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();

builder.Services.AddTransient<IItemGroupService, ItemGroupService>();
builder.Services.AddTransient<IItemGroupRepository, ItemGroupRepository>();

builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddTransient<IItemService, ItemService>();

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IJwtProvider, JwtProvider>();
builder.Services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
    
builder.Services.AddTransient<IReservationService, ReservationService>();
builder.Services.AddTransient<IReservationRepository, ReservationRepository>();

builder.Services.AddTransient<IItemVariationRepository, ItemVariationRepository>();

builder.Services.AddTransient<IOrderRepository, OrderRepository>();

builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();
builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<INotificationService, NotificationService>();

builder.Services.AddTransient<IRefundRepository, RefundRepository>();
builder.Services.AddTransient<IRefundService, RefundService>();

// Adding controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.ConfigureAuthentication();
builder.ConfigureStripe();

// Building app
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Adding middleware
app.UseExceptionHandling(
    new Dictionary<Type, HttpStatusCode>
    {
        { typeof(InvalidCredentialException), HttpStatusCode.Unauthorized },
        { typeof(InvalidRefreshTokenException), HttpStatusCode.Forbidden },
        { typeof(ExpiredRefreshTokenException), HttpStatusCode.Forbidden },
        { typeof(KeyNotFoundException), HttpStatusCode.NotFound },
        { typeof(TimeNotAvailableException), HttpStatusCode.BadRequest },
        { typeof(InvalidOrderStateException), HttpStatusCode.BadRequest },
        { typeof(InvalidUserRoleException), HttpStatusCode.BadRequest }
    });

//app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

// Mapping Controllers
app.MapControllers();

// Configuring Sqlite so that it is possible to push all the DB changes to the GitHub
app.ConfigureSqliteDatabase();

// Configuring auth
app.UseAuthentication();
app.UseAuthorization();

// Running app
app.Run();