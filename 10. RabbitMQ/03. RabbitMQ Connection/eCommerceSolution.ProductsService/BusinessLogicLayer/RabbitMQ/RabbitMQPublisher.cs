using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace eCommerce.ProductsService.BusinessLogicLayer.RabbitMQ;

public class RabbitMQPublisher : IRabbitMQPublisher, IDisposable
{
  private readonly IConfiguration _configuration;
  private readonly IModel _channel;
  private readonly IConnection _connection;

  public RabbitMQPublisher(IConfiguration configuration)
  {
    _configuration = configuration;

    string hostName = _configuration["RabbitMQ_HostName"]!;
    string userName = _configuration["RabbitMQ_UserName"]!;
    string password = _configuration["RabbitMQ_Password"]!;
    string port = _configuration["RabbitMQ_Port"]!;

    ConnectionFactory connectionFactory = new ConnectionFactory()
    {
      HostName = hostName,
      UserName = userName,
      Password = password,
      Port = Convert.ToInt32(port)
    };
    _connection = connectionFactory.CreateConnection();

    _channel = _connection.CreateModel();
  }


  public void Publish<T>(string routingKey, T message)
  {
    throw new NotImplementedException();
  }

  public void Dispose()
  {
    _channel.Dispose();
    _connection.Dispose();
  }
}
