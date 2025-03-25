using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.BusinessLogicLayer.ServiceContracts;

namespace eCommerce.ProductsMicroService.API.APIEndpoints;

public static class ProductAPIEndpoints
{
  public static IEndpointRouteBuilder MapProductAPIEndpoints(this IEndpointRouteBuilder app)
  {
    //GET /api/products
    app.MapGet("/api/products", async (IProductsService productsService) =>
    {
      List<ProductResponse?> products = await productsService.GetProducts();
      return Results.Ok(products);
    });


    //GET /api/products/search/product-id/00000000-0000-0000-0000-000000000000
    app.MapGet("/api/products/search/product-id/{ProductID}", async (IProductsService productsService, Guid ProductID) =>
    {
      ProductResponse? product = await productsService.GetProductByCondition(temp => temp.ProductID == ProductID);
      return Results.Ok(product);
    });


    return app;
  }
}
