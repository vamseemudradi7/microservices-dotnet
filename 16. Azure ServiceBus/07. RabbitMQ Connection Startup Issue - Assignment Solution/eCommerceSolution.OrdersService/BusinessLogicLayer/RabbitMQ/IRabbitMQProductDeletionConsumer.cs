namespace eCommerce.OrdersMicroservice.BusinessLogicLayer.RabbitMQ;

public interface IRabbitMQProductDeletionConsumer
{
  void Initialize(int delayMilliseconds);
  void Consume();
  void Dispose();
}

