using DbSRV.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<ApplicationContext>(options =>
//{
//    var connectionString = builder.Configuration.GetConnectionString("MariaDbConnectionString");
//    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//});

builder.Services.AddDbContextPool<ApplicationContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MariaDbConnectionString");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});


var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.Run();
