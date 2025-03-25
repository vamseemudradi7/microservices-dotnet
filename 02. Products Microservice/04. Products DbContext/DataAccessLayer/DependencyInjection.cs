﻿using eCommerce.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.ProductsService.DataAccessLayer;

public static class DependencyInjection
{
  public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
  {
    //TO DO: Add Data Access Layer services into the IoC container

    services.AddDbContext<ApplicationDbContext>(options => {
      options.UseMySQL(configuration.GetConnectionString("DefaultConnection")!);
    });
    return services;
  }
}
