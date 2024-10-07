using Microsoft.EntityFrameworkCore;
using NokoWebApi.Models;
using NokoWebApiExtra.Extensions.ApiRepository;
using NokoWebApiExtra.Repositories;

namespace NokoWebApi.Repositories;

[ApiRepository]
public class UserRepository(DbContextOptions<UserRepository> options) : BaseRepository<UserRepository, User>(options) 
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>((entity) =>
        {
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Phone).IsUnique();
        });
    }
}
