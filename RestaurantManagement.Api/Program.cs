using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Data;
using RestaurantManagement.Data.Contexts;
using RestaurantManagement.Data.Interface;
using RestaurantManagement.Domain.Configuration;
using Serilog;
using System.Reflection;

Log.Logger = new LoggerConfiguration()
                            .Enrich.FromLogContext()
                            .MinimumLevel.Information()
                            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                            .CreateLogger();
Log.Information("Configuring web host ({ApplicationContext})...", "Restaurant Management");

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("connectionstrings.json", optional: false, reloadOnChange: true);
});
var connectionString = builder.Configuration.GetSection("ConnectionStrings");

builder.Services.Configure<ConnectionStrings>(connectionString);
var restaurantConnection = connectionString.Get<ConnectionStrings>();

builder.Services.AddDbContext<RestaurantDbContext>(x => x.UseSqlServer(restaurantConnection.RestaurantSqlDb.ConnectionString));

// Add services to the container.

builder.Services.AddControllers();

var mapper = new MapperConfiguration(cfg =>
{
    cfg.AllowNullCollections = true;
    cfg.AddMaps(Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(Profile))));
}).CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<ICategoryData, CategoryData>();
builder.Services.AddScoped<IOrderData, OrderData>();
builder.Services.AddScoped<IRestaurantData, RestaurantData>();
builder.Services.AddScoped<IMenuData, MenuData>();
builder.Services.AddScoped<IAdminData, AdminData>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Task.Run(() =>
{
    using (var scope = app.Services.CreateScope())
    {
        var dataContext = scope.ServiceProvider.GetRequiredService<RestaurantDbContext>();
        dataContext.Database.Migrate();
    }
});

app.Run();
