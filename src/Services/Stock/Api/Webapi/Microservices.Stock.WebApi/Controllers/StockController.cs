using MediatR;
using Microservices.Application.Wrappers;
using Microservices.Stock.Application.Dto;
using Microservices.Stock.Application.Features.Commands.StockMotion;
using Microservices.WebApi;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Microservices.Stock.WebApi.Controllers
{

    [ApiController]
    public class StockController : BaseController
    {
        public StockController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("api/[controller]/addstock")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddStock(AvailableStockDto availableStock)
        {
            AddStockMotionCommand addStockMotionCommand = new AddStockMotionCommand();
            addStockMotionCommand.StockDtos = availableStock.StockDtos;
            ServiceResponse<bool> taskResult = await Mediator.Send(addStockMotionCommand);

            return Ok(taskResult);
        }

        [HttpPost]
        [Route("api/[controller]/resetstock")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ResetStock(AvailableStockDto availableStock)
        {
            ResetStockMotionCommand resetStockMotionCommand = new ResetStockMotionCommand();
            resetStockMotionCommand.StockDtos = availableStock.StockDtos;

            return Ok(await Mediator.Send(resetStockMotionCommand));
        }
    }
}
