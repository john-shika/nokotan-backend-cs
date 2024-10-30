using System.Collections;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NokoWebApiSdk.Annotations;
using NokoWebApiSdk.Cores.Utils;
using NokoWebApiSdk.Models;
using PrimaryKeyAttribute = NokoWebApiSdk.Annotations.PrimaryKeyAttribute;

namespace NokoWebApiSdk.Repositories;

public interface IMockList<T> : IEnumerable<T>
{
    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default);
    bool Contains(T item);
    void CopyTo(T[] array, int arrayIndex);
    int IndexOf(T item);
    void RemoveAt(int index);
    T this[int index] { get; set; }
}

public interface IMockDbCollection<TBaseModel> 
    where TBaseModel : BaseModel
{
    public LocalView<TBaseModel> Local { get; }
    public IAsyncEnumerable<TBaseModel> AsAsyncEnumerable();
    public IQueryable<TBaseModel> AsQueryable();
    public TBaseModel? Find(params object?[]? keyValues);
    public ValueTask<TBaseModel?> FindAsync(params object?[]? keyValues);
    public ValueTask<TBaseModel?> FindAsync(object?[]? keyValues, CancellationToken cancellationToken);
    public EntityEntry<TBaseModel> Add(TBaseModel entity);
    public ValueTask<EntityEntry<TBaseModel>> AddAsync(TBaseModel entity, CancellationToken cancellationToken = default);
    public EntityEntry<TBaseModel> Attach(TBaseModel entity);
    public EntityEntry<TBaseModel> Remove(TBaseModel entity);
    public EntityEntry<TBaseModel> Update(TBaseModel entity);
    public void AddRange(params TBaseModel[] entities);
    public Task AddRangeAsync(params TBaseModel[] entities);
    public void AttachRange(params TBaseModel[] entities);
    public void RemoveRange(params TBaseModel[] entities);
    public void UpdateRange(params TBaseModel[] entities);
    public void AddRange(IEnumerable<TBaseModel> entities);
    public Task AddRangeAsync(IEnumerable<TBaseModel> entities, CancellationToken cancellationToken = default);
    public void AttachRange(IEnumerable<TBaseModel> entities);
    public void RemoveRange(IEnumerable<TBaseModel> entities);
    public void UpdateRange(IEnumerable<TBaseModel> entities);
    public EntityEntry<TBaseModel> Entry(TBaseModel entity);
}

public interface IBaseRepository<TBaseModel> : IMockDbCollection<TBaseModel>, IMockList<TBaseModel>
    where TBaseModel : BaseModel
{
    public DbSet<TBaseModel> Db { get; set; }
}

public abstract class BaseRepository<TContext, TBaseModel>(DbContextOptions<TContext> options) : DbContext(options), IBaseRepository<TBaseModel>
    where TBaseModel : BaseModel 
    where TContext : DbContext
{
    public DbSet<TBaseModel> Db { get; set; }
    public LocalView<TBaseModel> Local => Db.Local;
    public int Count => Db.Count();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var baseModelType = typeof(BaseModel);
        // const string bModeIdName = nameof(BaseModel.Id);
        
        // Apply configurations to all entities derived from BaseModel
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var modelType = entityType.ClrType;
            
            if (!baseModelType.IsAssignableFrom(modelType)) continue;
            var entity = modelBuilder.Entity(modelType);
            
            // Apply UniqueKey configuration
            foreach (var property in modelType.GetProperties())
            {
                // High priority
                var primaryKey = property.GetCustomAttribute<PrimaryKeyAttribute>();
                if (primaryKey is not null)
                {
                    entity.HasKey(property.Name);
                    // entity.HasIndex(property.Name).IsUnique();
                    continue;
                }

                // Low Priority
                var uniqueKey = property.GetCustomAttribute<UniqueKeyAttribute>();
                if (uniqueKey is not null)
                {
                    entity.HasIndex(property.Name).IsUnique(uniqueKey.IsUnique);
                }
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
                    entry.Entity.DeletedAt = NokoCommonMod.GetDateTimeUtcNow();
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = NokoCommonMod.GetDateTimeUtcNow();
                    break;
                case EntityState.Added:
                    entry.Entity.Id = NokoCommonMod.GenerateUuidV7();
                    entry.Entity.CreatedAt = NokoCommonMod.GetDateTimeUtcNow();
                    entry.Entity.UpdatedAt = NokoCommonMod.GetDateTimeUtcNow();
                    break;
                default:
                    throw new Exception("Unknown EntityState");
            }
        }
        
        // Run Before Save Changes From Base Db Context
        return base.SaveChanges();
    }

    public IAsyncEnumerable<TBaseModel> AsAsyncEnumerable() 
    {
        return Db.AsAsyncEnumerable();
    }
    
    public IQueryable<TBaseModel> AsQueryable() 
    {
        return Db.AsQueryable();
    }
    
    public TBaseModel? Find(params object?[]? keyValues) 
    {
        return Db.Find(keyValues);
    }
    
    public ValueTask<TBaseModel?> FindAsync(params object?[]? keyValues) 
    {
        return Db.FindAsync(keyValues);
    }
    
    public ValueTask<TBaseModel?> FindAsync(object?[]? keyValues, CancellationToken cancellationToken) 
    {
        return Db.FindAsync(keyValues, cancellationToken);
    }
    
    public EntityEntry<TBaseModel> Add(TBaseModel entity) 
    {
        return Db.Add(entity);
    }

    public ValueTask<EntityEntry<TBaseModel>> AddAsync(TBaseModel entity, CancellationToken cancellationToken = default) 
    {
        return Db.AddAsync(entity, cancellationToken);
    }
    
    public EntityEntry<TBaseModel> Attach(TBaseModel entity) 
    {
        return Db.Attach(entity);
    }

    public EntityEntry<TBaseModel> Remove(TBaseModel entity)
    {
        return Db.Remove(entity);
    }

    public EntityEntry<TBaseModel> Update(TBaseModel entity)
    {
        return Db.Update(entity);
    }

    public virtual void AddRange(params TBaseModel[] entities)
    {
        Db.AddRange(entities);
    }

    public Task AddRangeAsync(params TBaseModel[] entities)
    {
        return Db.AddRangeAsync(entities);
    }

    public void AttachRange(params TBaseModel[] entities)
    {
        Db.AttachRange(entities);
    }

    public void RemoveRange(params TBaseModel[] entities)
    {
        Db.RemoveRange(entities);
    }

    public void UpdateRange(params TBaseModel[] entities)
    {
        Db.UpdateRange(entities);
    }

    public void AddRange(IEnumerable<TBaseModel> entities)
    {
        Db.AddRange(entities);
    }

    public Task AddRangeAsync(IEnumerable<TBaseModel> entities, CancellationToken cancellationToken = default)
    {
        return Db.AddRangeAsync(entities, cancellationToken);
    }

    public void AttachRange(IEnumerable<TBaseModel> entities)
    {
        Db.AttachRange(entities);
    }

    public void RemoveRange(IEnumerable<TBaseModel> entities)
    {
        Db.RemoveRange(entities);
    }

    public void UpdateRange(IEnumerable<TBaseModel> entities)
    {
        Db.UpdateRange(entities);
    }

    public EntityEntry<TBaseModel> Entry(TBaseModel entity)
    {
        return Db.Entry(entity);
    }

    public IAsyncEnumerator<TBaseModel> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return Db.GetAsyncEnumerator(cancellationToken);
    }

    public IEnumerator<TBaseModel> GetEnumerator()
    {
        return Db.OrderBy(x => x.Id).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public bool Contains(TBaseModel item) 
    {
        return Db.Contains(item);
    }
    
    // TODO: Method CopyTo Overload, Get Array And Copied
    public void CopyTo(TBaseModel[] array, int arrayIndex)
    {
        Db.ToArray().CopyTo(array, arrayIndex);
    }

    public int IndexOf(TBaseModel item)
    {
        var index = Db.OrderBy(x => x.Id)
            .Select((x, i) => new { x.Id, Index = i })
            .FirstOrDefault(x => x.Id == item.Id)?.Index ?? -1;
        return index;
    }

    public void RemoveAt(int index)
    {
        var item = this[index];
        Db.Update(item);
        SaveChanges();
    }

    public TBaseModel this[int index]
    {
        get
        {
            // if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
            return Db.OrderBy(x => x.Id).ElementAt(index);
        }
        set
        {
            value.Id = this[index].Id;
            Db.Update(value);
            SaveChanges();
        }
    }
}