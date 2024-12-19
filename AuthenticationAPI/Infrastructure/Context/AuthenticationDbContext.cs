using AuthenticationAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AuthenticationAPI.Infrastructure.Context;

public class AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

