using Application.Common.Models;
using Application.CustomerOrders.Command.AddCustomerOrder;
using Application.CustomerOrders.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerOrderController : ControllerBase
    {
        private readonly IMediator mediator;
        public CustomerOrderController(IMediator _mediator)
        {
            mediator = _mediator;
        }
        [HttpGet]
        public async Task<List<CustomerOrderVM>> GetAllCustomerOrders()
        {
            var result = await mediator.Send(new GetAllCustomerOrdersQuery());
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerOrder([FromBody] CreateCustomerOrderCommand command)
        {
            var result = await mediator.Send(command);
            if (result.Success)
                return Ok(new { OrderNumber = result.Data, Message = result.Message });

            return BadRequest(new { Errors = result.Errors, Message = result.Message });
        }
    }
}
