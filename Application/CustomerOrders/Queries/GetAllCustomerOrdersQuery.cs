using Application.Common.Interfaces;
using Application.CustomerOrders.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CustomerOrders.Queries
{
    public class GetAllCustomerOrdersQuery : IRequest<List<CustomerOrderVM>>
    {
    }
    public class GetAllCustomerOrdersQueryHandler : IRequestHandler<GetAllCustomerOrdersQuery, List<CustomerOrderVM>>
    {
        private readonly IApplicationDbContext _context;
        public GetAllCustomerOrdersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public Task<List<CustomerOrderVM>> Handle(GetAllCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = _context.CustomerOrders
                                .Include(order => order.SnacksOrder)
                                .Include(order => order.DrinksOrder)
                                .Include(order => order.DessertsOrder)
                                .Select(order => new CustomerOrderVM
                                {
                                    CustomerName = order.CustomerName,
                                    TableNumber = order.TableNumber,
                                    OrderNumber = order.OrderNumber,
                                    SnacksOrder = order.SnacksOrder,
                                    DessertsOrder = order.DessertsOrder,
                                    DrinksOrder = order.DrinksOrder
                                })
                                .ToListAsync();

            if (orders is null)
                return Task.FromResult(new List<CustomerOrderVM>());

            return orders;
        }
    }
}
