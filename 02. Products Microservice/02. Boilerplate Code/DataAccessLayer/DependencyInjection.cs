﻿using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.ProductsService.DataAccessLayer;

public static class DependencyInjection
{
  public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
  {
    //TO DO: Add Data Access Layer services into the IoC container

    return services;
  }
}
