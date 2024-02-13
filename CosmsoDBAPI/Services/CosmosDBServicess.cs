using CosmosDBAPI.Models;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Azure.Cosmos;

namespace CosmosDBAPI;

public class CosmosDBServicess : ICosmosDBServices
{
    private readonly CosmosClient _client;
    public CosmosDBServicess()
    {
        _client =
          new CosmosClient(accountEndpoint: "https://localhost:8081/",
                 authKeyOrResourceToken: "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");
    }

    private Container container
    {
        get => _client.GetDatabase("cosmicworks").GetContainer("products");
    }

    public async Task<IEnumerable<Product>> RetrieveAllProductsAsync()
    {
        var queryable = container.GetItemLinqQueryable<Product>();

        using FeedIterator<Product> feed = queryable.Where(p => p.price < 2000m)
                                                    .OrderByDescending(p => p.price)
                                                    .ToFeedIterator();
        List<Product> result = new();

        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            foreach (Product product in response)
            {
                result.Add(product);
            }
        }

        return result;

    }

    public async Task<IEnumerable<Product>> RetrieveActiveProductsAsync()
    {

        string sql = """
                            SELECT
                                p.id,
                                p.categoryId,
                                p.categoryName,
                                p.sku,
                                p.name,
                                p.description,
                                p.price,
                                p.tags
                            FROM products p
                            JOIN t IN p.tags
                            WHERE t.name = @tagFilter
                            """;

        var query = new QueryDefinition(query: sql).WithParameter("@tagFilter", "Tag-75");

        using FeedIterator<Product> feed = container.GetItemQueryIterator<Product>(queryDefinition: query);

        List<Product> result = new();

        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            foreach (Product product in response)
            {
                result.Add(product);
            }
        }

        return result;


    }
}
