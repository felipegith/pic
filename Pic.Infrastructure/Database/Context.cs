using Microsoft.EntityFrameworkCore;
using Pic.Domain;

namespace Pic.Infrastructure;

public class Context : DbContext
{
    private readonly PublishDomainEventInterceptor _publishDomainEventInterceptor;
    public Context(DbContextOptions<Context> options, PublishDomainEventInterceptor publishDomainEventInterceptor) : base(options)
    {
        _publishDomainEventInterceptor = publishDomainEventInterceptor;
    }
    public Context(){}
    public DbSet<User> Users {get; set;}
    public DbSet<Value> Values {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Ignore<List<IDomainEvent>>()
        .ApplyConfigurationsFromAssembly(typeof(Context).Assembly)
         .Entity<User>()
         .HasOne(v=>v.Value)
         .WithOne(u=>u.User)
         .HasForeignKey<Value>(v=>v.UserId);
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}
