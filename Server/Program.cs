using Microsoft.EntityFrameworkCore;
using Server;
using Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddSignalR(options =>
{
    options.ClientTimeoutInterval = TimeSpan.FromMinutes(10);
    options.KeepAliveInterval = TimeSpan.FromMinutes(10);
    options.HandshakeTimeout = TimeSpan.FromMinutes(10);
    options.EnableDetailedErrors = true;
});

var app = builder.Build();

app.MapHub<GameHub>("/game");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await ApplicationDbContextSeed.SeedQuestionsAsync(context);
}

app.Run();
