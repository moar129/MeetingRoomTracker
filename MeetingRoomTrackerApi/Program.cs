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
//var connectionString = "Server=mssql3.unoeuro.com;Database=devnoter_dk_db_dev_noter;User Id=devnoter_dk;Password=dhcED6fzFnR3A94GyHxb;Encrypt=True;TrustServerCertificate=True;";
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
builder.Services.AddDbContext<RMTDbContext>(options =>
    options.UseSqlServer(connectionString));


// Dependency Injection
builder.Services.AddScoped<ITimeLogService, TimeLogServce>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IRepos<Room>, RoomRepo>();
builder.Services.AddScoped<IRepos<TimeLog>, TimeLogRepo>();

var app = builder.Build();

// Use CORS policy
app.UseCors("AllowAll");

// Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Meeting Room Tracker API v1");
    options.RoutePrefix = "swagger";
});
// Redirect root Swagger UI
app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseAuthorization();
app.MapControllers();


app.Run();
