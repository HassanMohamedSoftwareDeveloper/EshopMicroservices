﻿namespace Ordering.Application.Extensions;

public static class OrderExtension
{
    public static List<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(order => new OrderDto(
            Id: order.Id.Value,
            CustomerId: order.CustomerId.Value,
            OrderName: order.OrderName.Value,
            ShippingAddress: new AddressDto(order.ShippingAddress.FirstName,
                                            order.ShippingAddress.LastName,
                                            order.ShippingAddress.EmailAddress,
                                            order.ShippingAddress.AddressLine,
                                            order.ShippingAddress.Country,
                                            order.ShippingAddress.State,
                                            order.ShippingAddress.ZipCode),
            BillingAddress: new AddressDto(order.BillingAddress.FirstName,
                                           order.BillingAddress.LastName,
                                           order.BillingAddress.EmailAddress,
                                           order.BillingAddress.AddressLine,
                                           order.BillingAddress.Country,
                                           order.BillingAddress.State,
                                           order.BillingAddress.ZipCode),
            Payment: new PaymentDto(order.Payment.CardName,
                                    order.Payment.CardNumber,
                                    order.Payment.Expiration,
                                    order.Payment.CVV,
                                    order.Payment.PaymentMethod),
            Status: order.Status,
            OrderItems: order.OrderItems.Select(orderItem => new OrderItemDto(orderItem.OrderId.Value,
                                                                              orderItem.ProductId.Value,
                                                                              orderItem.Quantity,
                                                                              orderItem.Price)).ToList()))
            .ToList();
    }
}