namespace Microservices.Stock.EventStore
{
    public interface IStockMotionEventStore
    {
        Task<bool> CreateStockMotionEvent(StockMotionEvent stockMotionEvent);
    }
}
