using Microsoft.EntityFrameworkCore;
using PokeHub.API.Models;

namespace PokeHub.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Hunt> Hunts { get; set; }

    public DbSet<User> Users { get; set; }
}