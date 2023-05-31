using Microservices.Persistence.Repository.EfCore;
using Microservices.Stock.Application.Dto;
using Microservices.Stock.Application.Repositories;
using Microservices.Stock.Persistence.Repositories.EfCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Stock.Persistence.Repositories.EfCore
{
    public class StockEfCoreRepository : BaseEfCoreRepository<StockDbContext, Domain.Models.Stock, StockSearchDto>, IStockRepository
    {
        public StockEfCoreRepository(StockDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Domain.Models.Stock> GetByProductId(StockSearchDto stockSearchDto)
        {
            return await Context.Stocks.Where(p => p.ProductId == stockSearchDto.ProductId).FirstOrDefaultAsync();
        }
    }
}
