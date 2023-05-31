using Microservices.Domain;
using Microservices.Persistence.Repository.EfCore.Extension;
using Microservices.Persistence.Repository.EfCore.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Microservices.Persistence.Repository.EfCore.Context
{
    public abstract class CoreDbContext : DbContext
    {
        public CoreDbContext()
        {
        }

        public CoreDbContext(DbContextOptions options) : base(options)
        {
        }
        public virtual void OnConfiguringDbContext(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.EnableSensitiveDataLogging();
#if DEBUG
                base.OnConfiguring(optionsBuilder.UseLoggerFactory(ContextLoggerFactory));
#endif
                OnConfiguringDbContext(optionsBuilder);
            }
        }

        public static readonly ILoggerFactory ContextLoggerFactory
            = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name
                        && level == LogLevel.Information)
                    .AddDebug();
            });
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(asm => asm.DefinedTypes).Select(x => x.AsType());

            var typesToRegister = types.Where(type => type.BaseType != null
            && type.BaseType.IsGenericType
            && type.BaseType.GetGenericTypeDefinition() == typeof(BaseEntityMap<>));

            foreach (var type in typesToRegister)
            {
                if (type.Name.Contains("BaseEntityMap")) continue;
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

            modelBuilder.AddGlobalDeletedFilter();
        }
        public override int SaveChanges()
        {
            ModifyBaseEntities().Wait();

            var result = base.SaveChanges();

            return result;
        }
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            await ModifyBaseEntities();

            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            return result;
        }
        private Task ModifyBaseEntities()
        {
            var modifiedEntries = ChangeTracker.Entries()
               .Where(x => (x.Entity is IEntity)
                   && (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted));

            foreach (var entry in modifiedEntries)
            {
                IEntity entity = entry.Entity as IEntity;

                if (entity != null)
                {
                    DateTime now = DateTime.UtcNow;

                    if (entry.State == EntityState.Added)
                    {
                        ((IEntity)entry.Entity).Id = ((IEntity)entry.Entity).Id == Guid.Empty ? Guid.NewGuid() : ((IEntity)entry.Entity).Id;
                        if (entry.Entity is IEntity)
                        {
                            ((IEntity)entry.Entity).CreationDate = now;
                            ((IEntity)entry.Entity).LastModificationDate = now;
                            ((IEntity)entry.Entity).IsDeleted = false;
                        }
                    }
                    else
                    {
                        if (entry.Entity is IEntity)
                        {
                            base.Entry((IEntity)entry.Entity).Property(x => x.CreationDate).IsModified = false;
                            ((IEntity)entry.Entity).LastModificationDate = now;
                            if (entry.State == EntityState.Deleted)
                            {
                                entry.State = EntityState.Modified;
                                ((IEntity)entry.Entity).IsDeleted = true;
                            }
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
