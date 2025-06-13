using FluentValidation;

namespace Application.CustomerOrders.Command.AddCustomerOrder
{
    public class CreateCustomerOrderCommandValidator : AbstractValidator<CreateCustomerOrderCommand>
    {
        public CreateCustomerOrderCommandValidator()
        {
            RuleFor(x => x.Order).NotNull().WithMessage("Order cannot be null");

            RuleFor(x => x.Order.CustomerName)
                .NotEmpty().WithMessage("Customer name is required");

            RuleFor(x => x.Order.TableNumber)
                .NotEmpty().WithMessage("Table number is required");

            RuleForEach(x => x.Order.SnacksOrder).SetValidator(new SnacksOrderDtoValidator());
            RuleForEach(x => x.Order.DrinksOrder).SetValidator(new DrinksOrderDtoValidator());
            RuleForEach(x => x.Order.DessertsOrder).SetValidator(new DessertsOrderDtoValidator());
        }
    }

    public class SnacksOrderDtoValidator : AbstractValidator<SnacksOrderDto>
    {
        public SnacksOrderDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.HalfQuantity).GreaterThanOrEqualTo((short)0);
            RuleFor(x => x.FullQuantity).GreaterThanOrEqualTo((short)0);
        }
    }

    public class DrinksOrderDtoValidator : AbstractValidator<DrinksOrderDto>
    {
        public DrinksOrderDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.HalfQuantity).GreaterThanOrEqualTo((short)0);
            RuleFor(x => x.FullQuantity).GreaterThanOrEqualTo((short)0);
        }
    }

    public class DessertsOrderDtoValidator : AbstractValidator<DessertsOrderDto>
    {
        public DessertsOrderDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.HalfQuantity).GreaterThanOrEqualTo((short)0);
            RuleFor(x => x.FullQuantity).GreaterThanOrEqualTo((short)0);
        }
    }
}
