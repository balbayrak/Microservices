using Microservices.Domain;

namespace Microservices.Persistence.Repository
{
    public abstract class BaseRepository<TEntity> where TEntity : class, IEntity
    {

    }
}