namespace eCommerce.OrdersMicroservice.BusinessLogicLayer.ServiceBus;

public interface IServiceBusProductUpdateConsumer
{
  Task ConsumeAsync();
}
