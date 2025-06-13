using Application.Common.Interfaces;
using Application.Common.Responses;
using Application.CustomerOrders.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;

namespace Application.CustomerOrders.Command.AddCustomerOrder
{
    public class CreateCustomerOrderCommand : IRequest<ServiceResponse<string>>
    {
        public CreateCustomerOrderDto Order { get; set; }
    }

    public class CreateCustomerOrderCommandHandler : IRequestHandler<CreateCustomerOrderCommand, ServiceResponse<string>>
    {
        private readonly IApplicationDbContext _context;

        public CreateCustomerOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<string>> Handle(CreateCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var orderId = Guid.NewGuid(); 

                var order = new CustomerOrder
                {
                    Id = orderId,
                    CustomerName = request.Order.CustomerName,
                    TableNumber = request.Order.TableNumber,
                    OrderNumber = Guid.NewGuid(),

                    SnacksOrder = request.Order.SnacksOrder.Select(so => new SnacksOrder
                    {
                        Name = so.Name,
                        HalfQuantity = so.HalfQuantity,
                        HalfPrice = so.HalfPrice,
                        FullQuantity = so.FullQuantity,
                        FullPrice = so.FullPrice,
                        CustomerOrderId = orderId
                    }).ToList(),

                    DrinksOrder = request.Order.DrinksOrder.Select(dor => new DrinksOrder
                    {
                        Name = dor.Name,
                        HalfQuantity = dor.HalfQuantity,
                        HalfPrice = dor.HalfPrice,
                        FullQuantity = dor.FullQuantity,
                        FullPrice = dor.FullPrice,
                        CustomerOrderId = orderId
                    }).ToList(),

                    DessertsOrder = request.Order.DessertsOrder.Select(de => new DessertsOrder
                    {
                        Name = de.Name,
                        HalfQuantity = de.HalfQuantity,
                        HalfPrice = de.HalfPrice,
                        FullQuantity = de.FullQuantity,
                        FullPrice = de.FullPrice,
                        CustomerOrderId = orderId
                    }).ToList()
                };

                _context.CustomerOrders.Add(order);
                await _context.SaveChangesAsync(cancellationToken);

                return ServiceResponse<string>.SuccessResponse(order.OrderNumber.ToString(),"Customer order created successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<string>.ExceptionResponse(ex, "Failed to create customer order.");
            }
        }
    }
}
