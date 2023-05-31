using MediatR;
using Microservices.Application.Wrappers;
using Microservices.Order.Application.Dto;
using Microservices.Order.Application.Features.Commands.Order;
using Microservices.WebApi;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Microservices.Order.WebApi.Controllers
{
    [ApiController]
    public class OrderController : BaseController
    {
        public OrderController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("api/[controller]/create")]
        [ProducesResponseType(typeof(ServiceResponse<Guid>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> Create(OrderCreateDto orderCreateDto)
        {
            var command = new CreateOrderCommand(orderCreateDto);
            var result = await Mediator.Send(command);
            
            return Ok(result);
        }
    }
}
