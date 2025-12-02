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
// Add Swagger generation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Meeting Room Tracker API", Version = "v1" });
});

// Database
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
builder.Services.AddDbContext<RMTDbContext>(options =>
    options.UseSqlServer(connectionString));

// Dependency Injection
builder.Services.AddScoped<ITimeLogService, TimeLogServce>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IRepos<Room>, RoomRepo>();
builder.Services.AddScoped<IRepos<TimeLog>, TimeLogRepo>();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    // Redirect root Swagger UI
    app.MapGet("/", () => Results.Redirect("/swagger"));
}
// Use CORS policy
app.UseCors("AllowAll");

// Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Meeting Room Tracker API v1");
    options.RoutePrefix = "swagger";
});

app.UseAuthorization();
app.MapControllers();


app.Run();
