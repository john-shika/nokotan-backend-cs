using Microsoft.EntityFrameworkCore;
using NokoWebApi.Models;
using NokoWebApiSdk.Annotations;
using NokoWebApiSdk.Repositories;

namespace NokoWebApi.Repositories;

[ApiRepository]
public class SessionRepository(DbContextOptions<SessionRepository> options) : BaseRepository<SessionRepository, Session>(options) 
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // modelBuilder.Entity<Session>((entity) =>
        // {
        //     entity.HasIndex(e => e.UserId).IsUnique();
        //     entity.HasIndex(e => e.Token).IsUnique();
        //     entity.HasIndex(e => e.NewToken).IsUnique();
        // });
    }
}