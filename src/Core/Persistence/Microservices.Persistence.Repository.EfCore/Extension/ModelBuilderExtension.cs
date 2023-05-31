using Microservices.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Microservices.Persistence.Repository.EfCore.Extension
{
    public static class ModelBuilderExtension
    {
        public static void AddGlobalDeletedFilter(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.SetDeleteFilter(entityType.ClrType);
                }
            }
        }
        public static void SetDeleteFilter(this ModelBuilder modelBuilder, Type entityType)
        {
            SetDeleteFilterMethod.MakeGenericMethod(entityType)
                .Invoke(null, new object[] { modelBuilder });
        }

        static readonly MethodInfo SetDeleteFilterMethod = typeof(ModelBuilderExtension)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetDeleteFilter");

        public static void SetDeleteFilter<TEntity>(this ModelBuilder modelBuilder)
            where TEntity : class, IEntity
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
