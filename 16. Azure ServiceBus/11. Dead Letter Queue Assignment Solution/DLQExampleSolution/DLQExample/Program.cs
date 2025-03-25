using Azure.Messaging.ServiceBus;
using System.Text.Json;

class Program
{
  private const string ServiceBusConnectionString = "your_servicebus_connection_string";
  private const string TopicName = "products.updates";
  private const string SubscriptionName = "products.updates.orders";
  private const string DeadLetterQueuePath = $"{TopicName}/subscriptions/{SubscriptionName}/$DeadLetterQueue";

  static async Task Main()
  {
    Console.WriteLine("Listening to Dead Letter Queue messages...");

    ServiceBusClient client = new ServiceBusClient(ServiceBusConnectionString);
    ServiceBusProcessor processor = client.CreateProcessor(DeadLetterQueuePath, new ServiceBusProcessorOptions()
    {
      AutoCompleteMessages = false
    });

    processor.ProcessMessageAsync += Processor_ProcessMessageAsync;
    processor.ProcessErrorAsync += Processor_ProcessErrorAsync;

    await processor.StartProcessingAsync();

    Console.WriteLine("Press any key to stop the processor");
    Console.ReadKey();

    await processor.StopProcessingAsync();
    await processor.DisposeAsync();
  }


  private static async Task Processor_ProcessMessageAsync(ProcessMessageEventArgs arg)
  {
    try
    {
      string body = arg.Message.Body.ToString();
      Console.WriteLine("\nMessage:");
      Console.WriteLine(body);

      Console.WriteLine("Message Properties:");
      foreach (var prop in arg.Message.ApplicationProperties)
      {
        Console.WriteLine($"Key: {prop.Key}, value: {prop.Value}");
      }

      Console.WriteLine("System Properties:");
      Console.WriteLine($"Message ID: {arg.Message.MessageId}");
      Console.WriteLine($"Enqueued Time: {arg.Message.EnqueuedTime}");
      Console.WriteLine($"Sequence Number: {arg.Message.SequenceNumber}");
      Console.WriteLine($"Dead Letter Reason: {arg.Message.DeadLetterReason}");
      Console.WriteLine($"Dead Letter Error Description: {arg.Message.DeadLetterErrorDescription}");
      Console.WriteLine("-----------------------------------------");

      await arg.CompleteMessageAsync(arg.Message);
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
      Console.WriteLine(ex.StackTrace);
    }
  }

  private static Task Processor_ProcessErrorAsync(ProcessErrorEventArgs arg)
  {
    Console.WriteLine($"Error: {arg.Exception.Message}");
    Console.WriteLine($"Error: {arg.Exception.StackTrace}");
    return Task.CompletedTask;
  }
}

