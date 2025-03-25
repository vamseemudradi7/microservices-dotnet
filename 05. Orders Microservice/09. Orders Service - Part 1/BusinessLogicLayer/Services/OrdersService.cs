﻿using AutoMapper;
using eCommerce.OrdersMicroservice.BusinessLogicLayer.DTO;
using eCommerce.OrdersMicroservice.BusinessLogicLayer.ServiceContracts;
using eCommerce.OrdersMicroservice.DataAccessLayer.Entities;
using eCommerce.OrdersMicroservice.DataAccessLayer.RepositoryContracts;
using FluentValidation;
using FluentValidation.Results;
using MongoDB.Driver;

namespace eCommerce.ordersMicroservice.BusinessLogicLayer.Services;

public class OrdersService : IOrdersService
{
  private readonly IValidator<OrderAddRequest> _orderAddRequestValidator;
  private readonly IValidator<OrderItemAddRequest> _orderItemAddRequestValidator;
  private readonly IValidator<OrderUpdateRequest> _orderUpdateRequestValidator;
  private readonly IValidator<OrderItemUpdateRequest> _orderItemUpdateRequestValidator;
  private readonly IMapper _mapper;
  private IOrdersRepository _ordersRepository;

  public OrdersService(IOrdersRepository ordersRepository, IMapper mapper, IValidator<OrderAddRequest> orderAddRequestValidator, IValidator<OrderItemAddRequest> orderItemAddRequestValidator, IValidator<OrderUpdateRequest> orderUpdateRequestValidator, IValidator<OrderItemUpdateRequest> orderItemUpdateRequestValidator)
  {
    _orderAddRequestValidator = orderAddRequestValidator;
    _orderItemAddRequestValidator = orderItemAddRequestValidator;
    _orderUpdateRequestValidator = orderUpdateRequestValidator;
    _orderItemUpdateRequestValidator = orderItemUpdateRequestValidator;
    _mapper = mapper;
    _ordersRepository = ordersRepository;
  }


  public async Task<OrderResponse?> AddOrder(OrderAddRequest orderAddRequest)
  {
    //Check for null parameter
    if (orderAddRequest == null)
    {
      throw new ArgumentNullException(nameof(orderAddRequest));
    }


    //Validate OrderAddRequest using Fluent Validations
    ValidationResult orderAddRequestValidationResult = await _orderAddRequestValidator.ValidateAsync(orderAddRequest);
    if (!orderAddRequestValidationResult.IsValid)
    {
      string errors = string.Join(", ", orderAddRequestValidationResult.Errors.Select(temp => temp.ErrorMessage));
      throw new ArgumentException(errors);
    }

    //Validate order items using Fluent Validation
    foreach (OrderItemAddRequest orderItemAddRequest in orderAddRequest.OrderItems)
    {
      ValidationResult orderItemAddRequestValidationResult = await _orderItemAddRequestValidator.ValidateAsync(orderItemAddRequest);

      if (!orderItemAddRequestValidationResult.IsValid)
      {
        string errors = string.Join(", ", orderItemAddRequestValidationResult.Errors.Select(temp => temp.ErrorMessage));
        throw new ArgumentException(errors);
      }
    }

    //TO DO: Add logic for checking if UserID exists in Users microservice


    //Convert data from OrderAddRequest to Order
    Order orderInput = _mapper.Map<Order>(orderAddRequest); //Map OrderAddRequest to 'Order' type (it invokes OrderAddRequestToOrderMappingProfile class)

    //Generate values
    foreach (OrderItem orderItem in orderInput.OrderItems) 
    {
      orderItem.TotalPrice = orderItem.Quantity * orderItem.UnitPrice;
    }
    orderInput.TotalBill = orderInput.OrderItems.Sum(temp => temp.TotalPrice);


    //Invoke repository
    Order? addedOrder = await _ordersRepository.AddOrder(orderInput);

    if (addedOrder == null) 
    {
      return null;
    }

    OrderResponse addedOrderResponse = _mapper.Map<OrderResponse>(addedOrder); //Map addedOrder ('Order' type) into 'OrderResponse' type (it invokes OrderToOrderResponseMappingProfile).

    return addedOrderResponse;
  }



  public async Task<OrderResponse?> UpdateOrder(OrderUpdateRequest orderUpdateRequest)
  {
    //Check for null parameter
    if (orderUpdateRequest == null)
    {
      throw new ArgumentNullException(nameof(orderUpdateRequest));
    }


    //Validate OrderAddRequest using Fluent Validations
    ValidationResult orderUpdateRequestValidationResult = await _orderUpdateRequestValidator.ValidateAsync(orderUpdateRequest);
    if (!orderUpdateRequestValidationResult.IsValid)
    {
      string errors = string.Join(", ", orderUpdateRequestValidationResult.Errors.Select(temp => temp.ErrorMessage));
      throw new ArgumentException(errors);
    }

    //Validate order items using Fluent Validation
    foreach (OrderItemUpdateRequest orderItemUpdateRequest in orderUpdateRequest.OrderItems)
    {
      ValidationResult orderItemUpdateRequestValidationResult = await _orderItemUpdateRequestValidator.ValidateAsync(orderItemUpdateRequest);

      if (!orderItemUpdateRequestValidationResult.IsValid)
      {
        string errors = string.Join(", ", orderItemUpdateRequestValidationResult.Errors.Select(temp => temp.ErrorMessage));
        throw new ArgumentException(errors);
      }
    }

    //TO DO: Add logic for checking if UserID exists in Users microservice


    //Convert data from OrderUpdateRequest to Order
    Order orderInput = _mapper.Map<Order>(orderUpdateRequest); //Map OrderUpdateRequest to 'Order' type (it invokes OrderUpdateRequestToOrderMappingProfile class)

    //Generate values
    foreach (OrderItem orderItem in orderInput.OrderItems)
    {
      orderItem.TotalPrice = orderItem.Quantity * orderItem.UnitPrice;
    }
    orderInput.TotalBill = orderInput.OrderItems.Sum(temp => temp.TotalPrice);


    //Invoke repository
    Order? updatedOrder = await _ordersRepository.UpdateOrder(orderInput);

    if (updatedOrder == null)
    {
      return null;
    }

    OrderResponse addedOrderResponse = _mapper.Map<OrderResponse>(updatedOrder); //Map addedOrder ('Order' type) into 'OrderResponse' type (it invokes OrderToOrderResponseMappingProfile).

    return addedOrderResponse;
  }
}