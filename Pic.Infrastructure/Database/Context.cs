using Microsoft.EntityFrameworkCore;
using Pic.Domain;

namespace Pic.Infrastructure;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options){}

    public DbSet<User> Users {get; set;}
}
