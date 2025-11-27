using MeetingRoomTrackerLib;
using MeetingRoomTrackerLib.Models;
using MeetingRoomTrackerLib.Repos;
using MeetingRoomTrackerLib.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with SQL Server
var connectionString = builder.Configuration["DB_CONNECTION_STRING"]
                       ?? builder.Configuration.GetConnectionString("DefaultConnection"); // Fallback to DefaultConnection if DB_CONNECTION_STRING is not set

builder.Services.AddDbContext<RMTDbContext>(options =>
    options.UseSqlServer(connectionString)); // Use SQL Server

builder.Services.AddScoped<ITimeLogService, TimeLogServce>();

builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IRepos<Room>, RoomRepo>();
builder.Services.AddScoped<IRepos<TimeLog>, TimeLogRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAll");

app.Run();
