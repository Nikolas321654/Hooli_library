using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using Microsoft.EntityFrameworkCore;
using HomeLib.Infrastructure;
using HomeLib.Infrastructure.Repositories;
using HomeLib.Services.Services;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<ITrackService, TrackService>();
builder.Services.AddScoped<ITrackRepository, TracksRepository>();
builder.Services.AddScoped<IArtistRepository, ArtistsRepository>();
builder.Services.AddScoped<IArtistsService, ArtistService>();
builder.Services.AddScoped<IArtistsService, ArtistService>();


builder.Services.AddDbContext<HomeLibDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("*** Hello from server! ***");

// app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();