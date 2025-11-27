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
builder.Services.AddEndpointsApiExplorer();
// Add Swagger generation - works in ALL environments
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Meeting Room Tracker API", Version = "v1" });
});

// Database
var connectionString = builder.Configuration["DB_CONNECTION_STRING"]
                       ?? builder.Configuration.GetConnectionString("DefaultConnection"); // Fallback to DefaultConnection if DB_CONNECTION_STRING is not set

builder.Services.AddDbContext<RMTDbContext>(options =>
    options.UseSqlServer(connectionString)); // Use SQL Server


// Dependency Injection
builder.Services.AddScoped<ITimeLogService, TimeLogServce>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IRepos<Room>, RoomRepo>();
builder.Services.AddScoped<IRepos<TimeLog>, TimeLogRepo>();

var app = builder.Build();

// Swagger middleware
app.UseSwagger();
// Enable Swagger UI at the app's root
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Meeting Room Tracker API v1");
    options.RoutePrefix = string.Empty;
});

// Generate OpenAPI document
app.MapOpenApi();
// Optional: Redirect root to Swagger UI (nice touch)
app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseAuthorization();
app.MapControllers();
// Use CORS policy
app.UseCors("AllowAll");

app.Run();
