using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace eCommerce.ProductsService.BusinessLogicLayer.ServiceBus;

public class ServiceBusPublisher : IServiceBusPublisher
{
  private readonly ServiceBusClient _serviceBusClient;
  private readonly IConfiguration _configuration;
  private readonly ServiceBusSender _sender;

  public ServiceBusPublisher(ServiceBusClient serviceBusClient, IConfiguration configuration)
  {
    _serviceBusClient = serviceBusClient;
    _configuration = configuration;

    _sender = _serviceBusClient.CreateSender(_configuration["ServiceBus:ServiceBus_Products_Topic"]);
  }


  public Task Publish<T>(Dictionary<string, object> headers, T message)
  {
    
  }
}

