using DbSRV.DB;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContextPool<ApplicationContext>(options => options
        .UseMySql(builder.Configuration.GetConnectionString("MariaDbConnectionString")
        ,ServerVersion.AutoDetect("MariaDbConnectionString")));

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.Run();
