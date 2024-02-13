
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Cosmos;

public class FamilyServices : IFamilyServices
{
    private readonly Container _family;
    public FamilyServices(CosmosClient cosmosClient)
    {
        _family = cosmosClient.GetDatabase("cosmicworks").GetContainer("family"); ;
    }

    public async Task<ItemResponse<Family>> CreateFamily(Family family)
    {
        return await _family.CreateItemAsync<Family>(family, new PartitionKey(family.LastName));
    }

    public async Task<ItemResponse<Family>> DeleteFamily(string id, string PartitionKey)
    {
        return await _family.DeleteItemAsync<Family>(id, new PartitionKey(PartitionKey));
    }

    public async Task<ItemResponse<Family>> GetFamily(string id, string PartitionKey)
    {
        return await _family.ReadItemAsync<Family>(id, new PartitionKey(PartitionKey));
    }

    public async Task<ItemResponse<Family>> UpdateIsRegistered(Family family, bool IsRegistered, string id, string PartitionKey)
    {
        ItemResponse<Family> wakefieldFamilyResponse = await _family.ReadItemAsync<Family>(id, new PartitionKey(PartitionKey));
        var itemBody = wakefieldFamilyResponse.Resource;

        // update registration status from false to true
        itemBody.IsRegistered = IsRegistered;

        // replace the item with the updated content
        return await _family.ReplaceItemAsync<Family>(itemBody, itemBody.Id, new PartitionKey(itemBody.LastName));

    }
}