using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Basket.Application.Dto
{
    public class BasketItemDto
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public BasketItemDto(Guid productId, string productName, decimal price, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            Price = price;
        }
    }
}
