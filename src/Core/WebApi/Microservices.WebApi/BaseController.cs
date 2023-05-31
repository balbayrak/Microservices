using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Microservices.WebApi
{
    public class BaseController : ControllerBase
    {
        protected IMediator Mediator { get; private set; }

        public BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
