﻿using Inkwave.Application.Interfaces.Repositories;

namespace Inkwave.Application.Features.Orders.Commands.OrderStates.OrderStateClosed;
internal class OrderStateClosedCommandHandler : IRequestHandler<OrderStateClosedCommand, Result<Order>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _OrderRepository;
    public OrderStateClosedCommandHandler(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
    {
        _unitOfWork = unitOfWork;
        _OrderRepository = orderRepository;
    }
    public async Task<Result<Order>> Handle(OrderStateClosedCommand request, CancellationToken cancellationToken)
    {
        var order = await _OrderRepository.GetOrderByIdAsync(request.OrderId);
        order.OrderStateContext.Closed();
        var result = await _unitOfWork.Save(cancellationToken);
        if (result > 0)
            return Result<Order>.Success();
        return Result<Order>.Failure();
    }
}
