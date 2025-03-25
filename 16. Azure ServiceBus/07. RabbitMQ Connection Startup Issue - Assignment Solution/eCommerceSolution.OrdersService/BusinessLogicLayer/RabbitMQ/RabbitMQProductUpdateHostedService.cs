using Microsoft.Extensions.Hosting;

namespace eCommerce.OrdersMicroservice.BusinessLogicLayer.RabbitMQ;

public class RabbitMQProductUpdateHostedService : IHostedService
{
  private readonly IRabbitMQProductUpdateConsumer _productNameUpdateConsumer;

  public RabbitMQProductUpdateHostedService(IRabbitMQProductUpdateConsumer consumer)
  {
    _productNameUpdateConsumer = consumer;
  }


  public Task StartAsync(CancellationToken cancellationToken)
  {
    _productNameUpdateConsumer.Initialize(4000);
    _productNameUpdateConsumer.Consume();

    return Task.CompletedTask;
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    _productNameUpdateConsumer.Dispose();

    return Task.CompletedTask;
  }
}
