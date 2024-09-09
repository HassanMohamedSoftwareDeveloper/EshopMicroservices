﻿using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints;
public record UpdateOrderRequest(OrderDto Order);
public record UpdateOrderResponse(bool IsSuccess);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateOrderCommand>();
            var result = await sender.Send(command);

            var response = result.Adapt<UpdateOrderRequest>();
            return Results.Ok(response);
        })
            .WithName("UpdateOrder")
            .Produces<UpdateOrderRequest>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Order")
            .WithDescription("Update Order");
    }
}
