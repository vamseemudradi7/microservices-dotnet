namespace eCommerce.ProductsService.BusinessLogicLayer.ServiceBus;

public interface IServiceBusPublisher
{
  Task Publish<T>(string topciName, Dictionary<string, object> headers, T message);
}

