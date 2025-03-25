using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using eCommerce.OrdersMicroservice.BusinessLogicLayer.Validators;
using eCommerce.ordersMicroservice.BusinessLogicLayer.Mappers;

namespace eCommerce.OrdersMicroservice.BusinessLogicLayer;

public static class DependencyInjection
{
  public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
  {
    //TO DO: Add business logic layer services into the IoC container
    services.AddValidatorsFromAssemblyContaining<OrderAddRequestValidator>();

    services.AddAutoMapper(typeof(OrderAddRequestToOrderMappingProfile).Assembly);

    return services;
  }
}