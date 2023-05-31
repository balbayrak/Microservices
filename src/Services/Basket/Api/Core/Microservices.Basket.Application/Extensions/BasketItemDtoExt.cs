using Microservices.Basket.Application.Dto;

namespace Microservices.Basket.Application.Extensions
{
    public static class BasketItemDtoExt
    {
        public static void AddBasketItems(this List<BasketItemDto> items, List<BasketItemDto> newItems)
        {
            var existedItems = newItems.Where(p => items.Any(t => t.ProductId == p.ProductId));
            foreach (var item in existedItems)
            {
                var existedItem = items.FirstOrDefault(p => p.ProductId == item.ProductId);
                if (existedItem != null)
                {
                    existedItem.Quantity += item.Quantity;
                }
            }
            var notExistedItems = newItems.Where(p => items.Any(t => t.ProductId != p.ProductId));
            if (notExistedItems.Any())
            {
                items.AddRange(notExistedItems);
            }

        }

        public static void AddBasketItem(this List<BasketItemDto> items, BasketItemDto item)
        {
            var existedItem = items.FirstOrDefault(p => p.ProductId == item.ProductId);
            if (existedItem != null)
            {
                existedItem.Quantity += item.Quantity;
            }
            else
            {
                items.Add(item);
            }
        }
    }
}
