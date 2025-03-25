using Azure.Messaging.ServiceBus;
using eCommerce.OrdersMicroservice.BusinessLogicLayer.DTO;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

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

  

  private async Task _serviceBusProcessor_ProcessMessageAsync(ProcessMessageEventArgs arg)
  {
    string messageBodyJson = arg.Message.Body.ToString();
    ProductDTO? productDTO = JsonSerializer.Deserialize<ProductDTO>(messageBodyJson);

    if (productDTO != null)
    {
      await HandleProductUpdation(productDTO);
    }

    // Complete the message to remove it from the queue
    await arg.CompleteMessageAsync(arg.Message);
  }

  private async Task HandleProductUpdation(ProductDTO productDTO)
  {
    _logger.LogInformation($"Product name updated: {productDTO.ProductID}, New name: {productDTO.ProductName}");

    string productJson = JsonSerializer.Serialize(productDTO);

    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
      .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

    string cacheKeyToWrite = $"product:{productDTO.ProductID}";

    await _distributedCache.SetStringAsync(cacheKeyToWrite, productJson, options);
  }

  private Task _serviceBusProcessor_ProcessErrorAsync(ProcessErrorEventArgs arg)
  {
    throw new NotImplementedException();
  }


  public async Task ConsumeAsync()
  {
    await _serviceBusProcessor.StartProcessingAsync();
  }

  public async void Dispose()
  {
    await _serviceBusProcessor.DisposeAsync();
  }
}
