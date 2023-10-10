using Microsoft.EntityFrameworkCore;
using Serilog;
using SimpleApISystem.Context;
using SimpleApISystem.Middlewares;
using SimpleApISystem.Repositories;
using SimpleApISystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");
var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(connectionString).Options;
var dbContext = new AppDbContext(dbContextOptions);
builder.Services.AddSingleton(dbContext);

builder.Services.AddSingleton<IPayLoadRepository, PayLoadRepository>();
builder.Services.AddSingleton<IPayLoadService, PayLoadService>();

Log.Logger = new LoggerConfiguration()
            .WriteTo.File("log.txt") 
            .CreateLogger();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();
