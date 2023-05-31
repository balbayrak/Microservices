using Microservices.Application.Wrappers;
using Microservices.Basket.Application.Dto;
using Microservices.Basket.Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Microservices.Basket.WebApi.Controllers
{
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/[controller]/getbasket")]
        [ProducesResponseType(typeof(ServiceResponse<BasketDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket(string buyerId)
        {
            var basket = await _repository.GetBasket(buyerId);
            return Ok(basket ?? new ServiceResponse<BasketDto>
            {
                IsSuccess = false,
                Value = null
            });
        }

        [HttpPost]
        [Route("api/[controller]/createbasket")]
        [ProducesResponseType(typeof(ServiceResponse<BasketDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateBasket(BasketDto basket)
        {
            return Ok(await _repository.SaveOrUpdate(basket));
        }

        [HttpDelete]
        [Route("api/[controller]/deletebasket")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string buyerId)
        {
            await _repository.Delete(buyerId);
            return Ok();
        }
    }
}
