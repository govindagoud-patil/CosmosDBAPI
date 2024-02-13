using CosmosDBAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace CosmosDBAPI;

[Route("[controller]")]
[ApiController]
public class ProductController : ControllerBase
{

    private readonly ICosmosDBServices _cosmosDBServices;
    public ProductController(ICosmosDBServices cosmosDBServices)
    {
        _cosmosDBServices = cosmosDBServices;
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> RetrieveAllProductsAsync()
    {
        return await _cosmosDBServices.RetrieveAllProductsAsync();

    }

    [HttpPost]
    public async Task<Product> CreateProudct(Product product)
    {
        return await _cosmosDBServices.CreateProduct(product);

    }

}
