using Microservices.Application.Dto;
using Microservices.Application.Repositories;
using Microservices.Application.Wrappers;
using Microservices.Domain;
using Microservices.Persistence.Repository.EfCore.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Microservices.Persistence.Repository.EfCore
{
    public class BaseEfCoreRepository<TDbContext, TEntity, TSearchEntity> : BaseRepository<TEntity>, IRepository<TEntity, TSearchEntity>
         where TEntity : class, IEntity
         where TSearchEntity : PagedSearchDto
         where TDbContext : CoreDbContext
    {

        protected TDbContext Context { get; }
        private DbSet<TEntity> _entities;

        protected virtual DbSet<TEntity> Entities => _entities ?? (_entities = Context.Set<TEntity>());
        protected virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public BaseEfCoreRepository(TDbContext dbContext)
        {
            Context = dbContext;
            _entities = Context.Set<TEntity>();
        }
        public async Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            Context.Remove(entity);
            if (autoSave)
                await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity != null)
            {
                Context.Remove(entity);
                if (autoSave)
                    await Context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<TEntity>> FilterBy(Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            return await Entities.Where(filterExpression).ToListAsync();
        }

        public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            return await Entities.Where(filterExpression).FirstOrDefaultAsync();
        }

        public async Task<PagedResponse<IReadOnlyList<TEntity>>> GetAllAsync(TSearchEntity searchEntity, CancellationToken cancellationToken = default)
        {
            PagedResponse<IReadOnlyList<TEntity>> result = new PagedResponse<IReadOnlyList<TEntity>>();
            try
            {
                IQueryable<TEntity> query;
                int countTask;
                int searchTask;

                query = TableNoTracking.AsQueryable();

                countTask = await query.CountAsync();


                if (searchEntity.Page >= 0 && searchEntity.PageSize > 0)
                {
                    searchTask = await query.CountAsync();

                    var skip = searchEntity.Page * searchEntity.PageSize;

                    query = query.Skip(skip).Take(searchEntity.PageSize);
                }
                else
                    searchTask = await query.CountAsync();


                result.Value = await query.ToListAsync();
                result.IsSuccess = true;
                result.TotalCount = countTask == searchTask ? countTask : searchTask;

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Errors = new List<string>();
                result.Errors.Add($"Hata Oluştu:{ex.Message}");
            }

            return result;
        }

        public async Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await Entities.FindAsync(id, cancellationToken);
        }

        public async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await Context.AddAsync(entity);
            if (autoSave)
                await Context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            Context.Update(entity);
            if (autoSave)
                await Context.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
