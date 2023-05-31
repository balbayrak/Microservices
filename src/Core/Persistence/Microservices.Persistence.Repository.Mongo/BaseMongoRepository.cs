using Microservices.Application.Dto;
using Microservices.Application.Repositories;
using Microservices.Application.Wrappers;
using Microservices.Domain;
using Microservices.Persistence.Repository.Mongo.Context;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Microservices.Persistence.Repository.Mongo
{
    public abstract class BaseMongoRepository<TEntity, TSearchEntity> : BaseRepository<TEntity>, IRepository<TEntity, TSearchEntity>
        where TEntity : class, IEntity
        where TSearchEntity : PagedSearchDto
    {
        protected readonly IMongoContext Context;
        protected IMongoCollection<TEntity> DbSet;

        protected BaseMongoRepository(IMongoContext context)
        {
            Context = context;

            DbSet = Context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async virtual Task<PagedResponse<IReadOnlyList<TEntity>>> GetAllAsync(TSearchEntity searchEntity,CancellationToken cancellationToken = default)
        {
            var filter = CreateFilteredQuery(searchEntity);
            var entityResult = await QueryByPage(DbSet, filter, searchEntity.Page, searchEntity.PageSize);
            return new PagedResponse<IReadOnlyList<TEntity>>
            {
                IsSuccess = true,
                TotalCount = entityResult.totalCount,
                Value = entityResult.readOnlyList
            };
        }

        public async virtual Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await Context.AddCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", entity.Id), cancellationToken));
            if (autoSave)
                await Context.SaveChanges();
        }

        public async virtual Task DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await Context.AddCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id), cancellationToken));
            if (autoSave)
                await Context.SaveChanges();
        }

        public async virtual Task<IEnumerable<TEntity>> FilterBy(Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            return (await DbSet.FindAsync(filterExpression, null, cancellationToken)).ToList();
        }

        public async virtual Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            return await (await DbSet.FindAsync(filterExpression, null, cancellationToken)).FirstOrDefaultAsync();
        }

        public async virtual Task<TEntity?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id), null, cancellationToken);
            return data.SingleOrDefault();
        }

        public async virtual Task<TEntity> InsertAsync(TEntity entity, bool autoSave, CancellationToken cancellationToken = default)
        {
            await Context.AddCommand(() => DbSet.InsertOneAsync(entity));
            if (autoSave)
                await Context.SaveChanges();

            return entity;
        }

        public async virtual Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await Context.AddCommand(() => DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", entity.Id), entity));
            if (autoSave)
                await Context.SaveChanges();

            return entity;
        }

        protected virtual FilterDefinition<TEntity> CreateFilteredQuery(TSearchEntity searchEntity)
        {
            return Builders<TEntity>.Filter.Empty;
        }

        private static async Task<(int totalCount,int totalPages, IReadOnlyList<TEntity> readOnlyList)> QueryByPage(IMongoCollection<TEntity> collection, FilterDefinition<TEntity> filter, int page, int pageSize)
        {
            var countFacet = AggregateFacet.Create("count",
                PipelineDefinition<TEntity, AggregateCountResult>.Create(new[]
                {
                PipelineStageDefinitionBuilder.Count<TEntity>()
                }));

            var dataFacet = AggregateFacet.Create("data",
                PipelineDefinition<TEntity, TEntity>.Create(new[]
                {
                PipelineStageDefinitionBuilder.Skip<TEntity>((page - 1) * pageSize),
                PipelineStageDefinitionBuilder.Limit<TEntity>(pageSize),
                }));

            filter = filter ?? Builders<TEntity>.Filter.Empty;
            var aggregation = await collection.Aggregate()
                .Match(filter)
                .Facet(countFacet, dataFacet)
                .ToListAsync();

            var count = aggregation.First()
                .Facets.First(x => x.Name == "count")
                .Output<AggregateCountResult>()
                ?.FirstOrDefault()
                ?.Count ?? 0;

            var totalPages = (int)count / pageSize;

            var data = aggregation.First()
                .Facets.First(x => x.Name == "data")
                .Output<TEntity>();

            return ((int)count,totalPages, data);
        }
    }
}
