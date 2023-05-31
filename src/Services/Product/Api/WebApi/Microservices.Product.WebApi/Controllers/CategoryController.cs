using MediatR;
using Microservices.Application.Wrappers;
using Microservices.Product.Application.Dto.Category;
using Microservices.Product.Application.Features.Commands.Category;
using Microservices.Product.Application.Features.Queries.Category;
using Microservices.WebApi;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Microservices.Product.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : BaseController
    {
        public CategoryController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Route("api/[controller]/all")]
        [ProducesResponseType(typeof(PagedResponse<List<CategoryDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProducts([FromQuery]CategorySearchDto categorySearchDto)
        {
            var query = new GetAllCategoriesQuery(categorySearchDto);
            return Ok(await Mediator.Send(query));
        }

        [HttpGet]
        [Route("api/[controller]/read")]
        [ProducesResponseType(typeof(ServiceResponse<CategoryDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(Guid categoryId)
        {
            var query = new GetByIdCategoryQuery(categoryId);
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        [Route("api/[controller]/create")]
        public async Task<IActionResult> Create(CreateCategoryDto createCategoryDto)
        {
            var query = new CreateCategoryCommand(createCategoryDto);
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        [Route("api/[controller]/update")]
        public async Task<IActionResult> Update(UpdateCategoryDto updateCategoryDto)
        {
            var query = new UpdateCategoryCommand(updateCategoryDto);
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        [Route("api/[controller]/delete")]
        public async Task<IActionResult> Delete(Guid categoryId)
        {
            var query = new DeleteCategoryCommand(categoryId);
            return Ok(await Mediator.Send(query));
        }
    }
}
