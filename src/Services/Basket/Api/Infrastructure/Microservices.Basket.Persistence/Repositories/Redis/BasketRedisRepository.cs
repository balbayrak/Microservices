using Microservices.Application.Wrappers;
using Microservices.Basket.Application.Dto;
using Microservices.Basket.Application.Extensions;
using Microservices.Basket.Application.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Microservices.Basket.Persistence.Repositories.Redis
{
    public class BasketRedisRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRedisRepository(IDistributedCache cache)
        {
            _redisCache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<ServiceResponse<bool>> Delete(string buyerId)
        {
            await _redisCache.RemoveAsync(buyerId);

            return new ServiceResponse<bool>(true);
        }

        public async Task<ServiceResponse<BasketDto>> GetBasket(string buyerId)
        {
            var basket = await _redisCache.GetStringAsync(buyerId);

            if (String.IsNullOrEmpty(basket))
                return new ServiceResponse<BasketDto>
                {
                    IsSuccess = false,
                    Value = null
                };

            return new ServiceResponse<BasketDto>
            {
                IsSuccess = true,
                Value = JsonConvert.DeserializeObject<BasketDto>(basket)
            };
        }

        public async Task<ServiceResponse<BasketDto>> SaveOrUpdate(BasketDto basketDto)
        {
            var existedBasketResult = await GetBasket(basketDto.BuyerId);

            if (existedBasketResult != null && existedBasketResult.Value != null)
            {
                existedBasketResult.Value.BasketItems.AddBasketItems(basketDto.BasketItems);
                await _redisCache.SetStringAsync(basketDto.BuyerId, JsonConvert.SerializeObject(existedBasketResult.Value));
            }
            else
            {
                await _redisCache.SetStringAsync(basketDto.BuyerId, JsonConvert.SerializeObject(basketDto));
            }

            return await GetBasket(basketDto.BuyerId);
        }
    }
}
