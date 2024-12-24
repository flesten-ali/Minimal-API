using Microsoft.EntityFrameworkCore;
using MinimalAPI.Modles;
namespace MinimalAPI.DbContexts;

public class MinimalDbContext : DbContext
{
    public MinimalDbContext(DbContextOptions<MinimalDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
}
