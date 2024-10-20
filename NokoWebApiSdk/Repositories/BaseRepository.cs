using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NokoWebApiSdk.Annotations;
using NokoWebApiSdk.Cores;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Models;

namespace NokoWebApiSdk.Repositories;

public interface IBaseRepository<T> 
    where T : BaseModel
{
    public DbSet<T> Dataset { get; set; }
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
        const string baseModeIdName = nameof(BaseModel.Id);
        
        // Apply configurations to all entities derived from BaseModel
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!baseModelType.IsAssignableFrom(entityType.ClrType)) continue;
            var entity = modelBuilder.Entity(entityType.ClrType);

            // Model T Generic Has Key Base Model ID
            entity.HasKey(baseModeIdName);
            
            // Apply UniqueKey configuration
            foreach (var property in entityType.ClrType.GetProperties())
            {
                var uniqueKeyAttr = property.GetCustomAttribute<UniqueKeyAttribute>();
                if (uniqueKeyAttr is { IsUnique: true })
                {
                    entity.HasIndex(property.Name).IsUnique();
                }
            }
        }
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<BaseModel>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Id = NokoWebCommon.GenerateUuidV7();
                    entry.Entity.CreatedAt = NokoWebCommon.GetDateTimeUtcNow();
                    entry.Entity.UpdatedAt = NokoWebCommon.GetDateTimeUtcNow();
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