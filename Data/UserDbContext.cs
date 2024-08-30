using ACIDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace ACIDemo.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}
