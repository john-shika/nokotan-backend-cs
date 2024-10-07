using Microsoft.EntityFrameworkCore;
using NokoWebApiExtra.Models;
using NokoWebApiExtra.Utils;

namespace NokoWebApiExtra.Repositories;

public interface IBaseRepository<T> 
    where T : IBaseModel
{
}

public abstract class BaseRepository<TContext, TBaseModel>(DbContextOptions<TContext> options) : DbContext(options), IBaseRepository<TBaseModel>
    where TBaseModel : BaseModel 
    where TContext : DbContext
{
    public DbSet<TBaseModel> Dataset { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var baseModelType = typeof(BaseModel);
        const string nameBaseModeId = nameof(BaseModel.Id);
        
        // Apply configurations to all entities derived from BaseModel
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!baseModelType.IsAssignableFrom(entityType.ClrType)) continue;
            var entity = modelBuilder.Entity(entityType.ClrType);

            // Model T Generic Has Key Base Model ID
            entity.HasKey(nameBaseModeId);
        }
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<BaseModel>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Id = Common.GenerateUuidV7();
                    entry.Entity.CreatedAt = Common.GetDateTimeUtcNowInMilliseconds();
                    entry.Entity.UpdatedAt = Common.GetDateTimeUtcNowInMilliseconds();
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    throw new Exception("Unknown EntityState");
            }
        }
        
        // Run Before Save Changes From Base Db Context
        return base.SaveChanges();
    }
}