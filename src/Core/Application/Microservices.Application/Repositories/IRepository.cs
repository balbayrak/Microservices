using Microservices.Application.Dto;
using Microservices.Application.Wrappers;
using Microservices.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Application.Repositories
{
    public interface IRepository<TEntity, TSearchDto>
       where TEntity : class, IEntity
       where TSearchDto : PagedSearchDto
    {
        Task<PagedResponse<IReadOnlyList<TEntity>>> GetAllAsync(TSearchDto searchEntity, CancellationToken cancellationToken = default);
        Task<TEntity?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> FilterBy(Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken = default);
        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken = default);
        Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default);
    }
}
