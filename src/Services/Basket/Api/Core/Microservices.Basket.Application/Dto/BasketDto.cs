using Microservices.Basket.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Basket.Application.Dto
{
    public class BasketDto
    {
        public string BuyerId { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }

        public decimal TotalPrice
        {
            get => BasketItems.Sum(x => x.Price * x.Quantity);
        }

        public BasketDto(string buyerId)
        {
            this.BuyerId = buyerId;
        }

        public void AddBasketItem(BasketItemDto basketItemDto)
        {
            BasketItems = BasketItems ?? new List<BasketItemDto>();
            BasketItems.AddBasketItem(basketItemDto);
        }

        public void AddBasketItems(List<BasketItemDto> basketItemDtoList)
        {
            BasketItems = BasketItems ?? new List<BasketItemDto>();
            BasketItems.AddBasketItems(basketItemDtoList);
        }
    }
}
