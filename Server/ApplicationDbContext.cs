using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server;

public class ApplicationDbContext : DbContext
{
    public DbSet<Question> Questions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<GameSession> Sessions { get; set; }
    public DbSet<GameState> States { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}