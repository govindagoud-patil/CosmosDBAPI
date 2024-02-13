using Microsoft.Azure.Cosmos;

public interface IFamilyServices
{
    public Task<ItemResponse<Family>> CreateFamily(Family family);

    public Task<ItemResponse<Family>> GetFamily(string id, string PartitionKey);

    public Task<ItemResponse<Family>> DeleteFamily(string id, string PartitionKey);

    public Task<ItemResponse<Family>> UpdateIsRegistered(Family family, bool IsRegistered, string id, string PartitionKey);


}