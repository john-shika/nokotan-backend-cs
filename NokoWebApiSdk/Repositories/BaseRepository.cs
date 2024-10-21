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
        // const string baseModeIdName = nameof(BaseModel.Id);
        
        // Apply configurations to all entities derived from BaseModel
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var modelType = entityType.ClrType;
            
            if (!baseModelType.IsAssignableFrom(modelType)) continue;
            var entity = modelBuilder.Entity(modelType);
            
            // Apply UniqueKey configuration
            foreach (var property in modelType.GetProperties())
            {
                var uniqueKeyAttr = property.GetCustomAttribute<UniqueKeyAttribute>();
                if (uniqueKeyAttr is null) continue; 
                
                // Set Index Column By Property Name With Unique Key Attribute
                entity.HasIndex(property.Name).IsUnique(uniqueKeyAttr.IsUnique);
            }
        }
    }

    public override int SaveChanges()
    {
        // Need T Generic with Assembly Reflection to get all Entries Inherit with Base Model 
        
        foreach (var entry in ChangeTracker.Entries<BaseModel>())
        {
            switch (entry.State)
            {
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    entry.Entity.DeletedAt = NokoWebCommon.GetDateTimeUtcNow();
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = NokoWebCommon.GetDateTimeUtcNow();
                    break;
                case EntityState.Added:
                    entry.Entity.Id = NokoWebCommon.GenerateUuidV7();
                    entry.Entity.CreatedAt = NokoWebCommon.GetDateTimeUtcNow();
                    entry.Entity.UpdatedAt = NokoWebCommon.GetDateTimeUtcNow();
                    break;
                default:
                    throw new Exception("Unknown EntityState");
            }
        }
        
        // Run Before Save Changes From Base Db Context
        return base.SaveChanges();
    }
}