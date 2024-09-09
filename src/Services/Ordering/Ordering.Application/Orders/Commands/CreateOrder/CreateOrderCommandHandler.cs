﻿namespace Ordering.Application.Orders.Commands.CreateOrder;

internal sealed class CreateOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    #region Methods :

    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(command.Order);

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id.Value);
    }

    #endregion Methods :

    #region Helpers :

    private static Order CreateNewOrder(OrderDto orderDto)
    {
        var shippingAddress = Address.Of(orderDto.ShippingAddress.FirstName,
                                         orderDto.ShippingAddress.LastName,
                                         orderDto.ShippingAddress.EmailAddress,
                                         orderDto.ShippingAddress.AddressLine,
                                         orderDto.ShippingAddress.Country,
                                         orderDto.ShippingAddress.State,
                                         orderDto.ShippingAddress.State);

        var billingAddress = Address.Of(orderDto.BillingAddress.FirstName,
                                        orderDto.BillingAddress.LastName,
                                        orderDto.BillingAddress.EmailAddress,
                                        orderDto.BillingAddress.AddressLine,
                                        orderDto.BillingAddress.Country,
                                        orderDto.BillingAddress.State,
                                        orderDto.BillingAddress.State);

        var payment = Payment.Of(orderDto.Payment.CardName,
                                 orderDto.Payment.CardNumber,
                                 orderDto.Payment.Expiration,
                                 orderDto.Payment.Cvv,
                                 orderDto.Payment.PaymentMethod);

        var newOrder = Order.Create(OrderId.Of(Guid.NewGuid()),
                                    CustomerId.Of(orderDto.CustomerId),
                                    OrderName.Of(orderDto.OrderName),
                                    shippingAddress,
                                    billingAddress,
                                    payment);

        foreach (var orderItemDto in orderDto.OrderItems)
        {
            newOrder.Add(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);
        }

        return newOrder;
    }

    #endregion Helpers :
}