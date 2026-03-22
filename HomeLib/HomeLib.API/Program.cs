using System.Text.Json;
using System.Text.Json.Serialization;
using HomeLib.API.Middleware;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using Microsoft.EntityFrameworkCore;
using HomeLib.Infrastructure;
using HomeLib.Infrastructure.Repositories;
using HomeLib.Services;
using HomeLib.Services.Services;
using HomeLib.Core.Application.Artists.Handlers;
using Microsoft.AspNetCore.Http.Json;


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

// Handlers for Artist
builder.Services.AddScoped<AddArtistCommandHandler>();
builder.Services.AddScoped<UpdateArtistCommandHandler>();
builder.Services.AddScoped<SoftDeleteArtistCommandHandler>();
builder.Services.AddScoped<HardDeleteArtistCommandHandler>();
builder.Services.AddScoped<GetAllArtistsQueryHandler>();
builder.Services.AddScoped<GetArtistByIdQueryHandler>();
builder.Services.AddScoped<GetArtistAlbumsQueryHandler>();
builder.Services.AddScoped<GetArtistTracksQueryHandler>();
builder.Services.AddScoped<GetArtistWithAlbumsTracksQueryHandler>();

builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();

builder.Services.AddScoped<IPlaylistService, PlaylistService>();
builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();

builder.Services.AddScoped<JwtService>();
builder.Services.Configure<AuthSettings>(configuration.GetSection("AuthSettings"));
builder.Services.AddAuth(configuration);

builder.Services.AddDbContext<HomeLibDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HomeLibDbContext>();
    try
    {
        Console.WriteLine("Applying migrations...");
        dbContext.Database.Migrate();
        Console.WriteLine("Migrations applied successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error applying migrations: {ex.Message}");
        throw;
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("*** Hello from 'Hooli' server! ***");

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();