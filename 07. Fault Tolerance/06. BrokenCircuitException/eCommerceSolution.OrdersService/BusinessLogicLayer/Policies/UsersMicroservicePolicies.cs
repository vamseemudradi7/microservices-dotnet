﻿using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace eCommerce.OrdersMicroservice.BusinessLogicLayer.Policies;

public class UsersMicroservicePolicies : IUsersMicroservicePolicies
{
  private readonly ILogger<UsersMicroservicePolicies> _logger;

  public UsersMicroservicePolicies(ILogger<UsersMicroservicePolicies> logger)
  {
    _logger = logger;
  }


  public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
  {
    AsyncRetryPolicy<HttpResponseMessage> policy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
  .WaitAndRetryAsync(
     retryCount: 5, //Number of retries
     sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // Delay between retries
     onRetry: (outcome, timespan, retryAttempt, context) =>
     {
       _logger.LogInformation($"Retry {retryAttempt} after {timespan.TotalSeconds} seconds");
     });

    return policy;
  }


  public IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
  {
    AsyncCircuitBreakerPolicy<HttpResponseMessage> policy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
  .CircuitBreakerAsync(
     handledEventsAllowedBeforeBreaking: 3, //Threshold for failed requests
     durationOfBreak: TimeSpan.FromMinutes(2), // Waiting time to be in "Open" state
     onBreak: (outcome, timespan) =>
     {
       _logger.LogInformation($"Circuit breaker opened for {timespan.TotalMinutes} minutes due to consecutive 3 failures. The subsequent requests will be blocked");
     },
     onReset: () => {
       _logger.LogInformation($"Circuit breaker closed. The subsequent requests will be allowed.");
     });

    return policy;
  }
}
