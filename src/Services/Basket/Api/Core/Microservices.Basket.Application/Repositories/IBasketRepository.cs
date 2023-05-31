using Microservices.Application.Wrappers;
using Microservices.Basket.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Basket.Application.Repositories
{
    public interface IBasketRepository
    {
        Task<ServiceResponse<BasketDto>> GetBasket(string buyerId);

        Task<ServiceResponse<BasketDto>> SaveOrUpdate(BasketDto basketDto);

        Task<ServiceResponse<bool>> Delete(string buyerId);
    }
}
