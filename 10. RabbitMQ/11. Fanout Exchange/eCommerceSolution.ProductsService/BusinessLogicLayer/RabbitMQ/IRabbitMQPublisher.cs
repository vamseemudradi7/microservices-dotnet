﻿namespace eCommerce.ProductsService.BusinessLogicLayer.RabbitMQ;

public interface IRabbitMQPublisher
{
  void Publish<T>(string routingKey, T message);
}
