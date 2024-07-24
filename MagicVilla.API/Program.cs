using MagicVilla.API;
using MagicVilla.API.Data;
using MagicVilla.API.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// Logger setup
//Log.Logger = new LoggerConfiguration().MinimumLevel.Error().WriteTo.File("logs/VillaApiProjectLogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();
//builder.Host.UseSerilog();


// Add services to the container.
builder.Services.AddDbContext<VillaDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"))
    );

builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IVillaRepository, VillaRepository>();

//builder.Services.AddScoped<>



builder.Services.AddControllers();


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

app.Run();
