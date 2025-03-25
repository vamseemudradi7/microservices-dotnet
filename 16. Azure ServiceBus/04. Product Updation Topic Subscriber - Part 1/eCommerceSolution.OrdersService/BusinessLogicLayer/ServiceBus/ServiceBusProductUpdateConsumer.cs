using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace eCommerce.OrdersMicroservice.BusinessLogicLayer.ServiceBus;

public class ServiceBusProductUpdateConsumer : IServiceBusProductUpdateConsumer
{
  private readonly ServiceBusProcessor _serviceBusProcessor;
  private readonly IDistributedCache _distributedCache;
  private readonly ILogger<ServiceBusProductUpdateConsumer> _logger;
  private readonly IConfiguration _configuration;

  public ServiceBusProductUpdateConsumer(IDistributedCache distributedCache, ILogger<ServiceBusProductUpdateConsumer> logger, ServiceBusClient serviceBusClient, IConfiguration configuration)
  {
    _distributedCache = distributedCache;
    _logger = logger;
    _configuration = configuration;

    _serviceBusProcessor = serviceBusClient.CreateProcessor(
      _configuration["ServiceBus:ServiceBus_Products_Updates_Topic"], 
      _configuration["ServiceBus:ServiceBus_Products_Updates_Topic_Orders_Subscriptions"], 
      new ServiceBusProcessorOptions()
      { 
        AutoCompleteMessages = false 
      });

    _serviceBusProcessor.ProcessMessageAsync += _serviceBusProcessor_ProcessMessageAsync;

    _serviceBusProcessor.ProcessErrorAsync += _serviceBusProcessor_ProcessErrorAsync;
  }

  

  private Task _serviceBusProcessor_ProcessMessageAsync(ProcessMessageEventArgs arg)
  {
    throw new NotImplementedException();
  }

  private Task _serviceBusProcessor_ProcessErrorAsync(ProcessErrorEventArgs arg)
  {
    throw new NotImplementedException();
  }


  public async Task ConsumeAsync()
  {
    
  }
}
