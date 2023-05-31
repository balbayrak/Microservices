using Microservices.Application.Repositories;
using Microservices.Stock.Application.Dto;

namespace Microservices.Stock.Application.Repositories
{
    public interface IStockRepository : IRepository<Domain.Models.Stock, StockSearchDto>
    {
        Task<Domain.Models.Stock> GetByProductId(StockSearchDto stockSearchDto);
    }
}
