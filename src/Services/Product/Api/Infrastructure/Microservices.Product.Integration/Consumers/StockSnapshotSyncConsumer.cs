using AutoMapper;
using MassTransit;
using MediatR;
using Microservices.DistributedLock;
using Microservices.Integration;
using Microservices.Product.Application.Dto.Product;
using Microservices.Product.Application.Features.Queries.Product;
using Microservices.Product.IntegrationEvents.StockSnapshot;
using Microservices.Stock.IntegrationEvents.Stock;

namespace Microservices.Product.Integration.Consumers
{
    public class StockSnapshotSyncConsumer : IConsumer<StockSnapshotIncreasedEvent>,
                                              IConsumer<StockSnapshotDecreasedEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IIntegrationEventPublisher _integrationPublisher;
        private readonly IDistributedLockManager _distributedLockManager;
        public StockSnapshotSyncConsumer(IMediator mediator,
            IMapper mapper,
            IIntegrationEventPublisher integrationPublisher, IDistributedLockManager distributedLockManager)
        {
            _mediator = mediator;
            _mapper = mapper;
            _integrationPublisher = integrationPublisher;
            _distributedLockManager = distributedLockManager;
        }

        private string StockSnapshotOperationLockKey(Guid productId) => $"stock-snapshot-operation-lock-{productId}";

        public async Task Consume(ConsumeContext<StockSnapshotIncreasedEvent> context)
        {
            StockSnapshotIncreasedEvent stockSnapshotIncreasedEvent = context.Message;
            await ProductStockChanged(stockSnapshotIncreasedEvent.ProductItems, true);
        }

        public async Task Consume(ConsumeContext<StockSnapshotDecreasedEvent> context)
        {
            StockSnapshotDecreasedEvent stockSnapshotIncreasedEvent = context.Message;
            await ProductStockChanged(stockSnapshotIncreasedEvent.ProductItems, false);
        }

        private async Task ProductStockChanged(IEnumerable<ProductListItem> productItems, bool isIncreased)
        {
            List<StockListItem> stockListItems = new List<StockListItem>();
            foreach (var productItem in productItems)
            {
                var query = new GetByIdProductQuery(productItem.ProductId);
                var productResult = await _mediator.Send(query);
                if (productResult.IsSuccess && productResult.Value != null)
                {
                    var productDto = productResult.Value;
                    var updateProductDto = _mapper.Map<UpdateProductDto>(productDto);

                    await _distributedLockManager.LockAsync(StockSnapshotOperationLockKey(productItem.ProductId),
                               async () =>
                               {
                                   var productDto = productResult.Value;
                                   var updateProductDto = _mapper.Map<UpdateProductDto>(productDto);

                                   if (isIncreased)
                                       updateProductDto.StockSnapshot.AvailableStock += productItem.Count;
                                   else
                                       updateProductDto.StockSnapshot.AvailableStock -= productItem.Count;

                                   stockListItems.Add(new StockListItem
                                   {
                                       ProductId = updateProductDto.Id,
                                       Count = updateProductDto.StockSnapshot.AvailableStock
                                   });
                               });
                }

            }

            if (stockListItems.Count > 0)
            {
                _integrationPublisher.AddEvent(new StockSyncEvent
                {
                    StockList = stockListItems
                });

                await _integrationPublisher.Publish();
            }
        }
    }
}
