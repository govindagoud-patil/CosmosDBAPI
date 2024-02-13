using System.ComponentModel.DataAnnotations;
using CosmosDBAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;


namespace CosmosDBAPI;

[Route("[controller]")]
[ApiController]
public class FamilyController : ControllerBase
{

    private readonly IFamilyServices _familyServices;
    public FamilyController(IFamilyServices familyServices)
    {
        _familyServices = familyServices;
    }

    [HttpPost]
    public async Task<ItemResponse<Family>> CreateFamily(Family family)
    {
        return await _familyServices.CreateFamily(family);
    }

    [HttpGet]
    public async Task<ItemResponse<Family>> GetFamily([Required] string id, [Required] string PartitionKey)
    {
        return await _familyServices.GetFamily(id, PartitionKey);
    }

    [HttpPut]
    public async Task<ItemResponse<Family>> UpdateIsRegistered(Family family, bool IsRegistered, [Required] string id, [Required] string PartitionKey)
    {
        return await _familyServices.UpdateIsRegistered(family, IsRegistered, id, PartitionKey);
    }


    [HttpDelete]
    public async Task<ItemResponse<Family>> DeleteFamily([Required] string id, [Required] string PartitionKey)
    {
        return await _familyServices.DeleteFamily(id, PartitionKey);
    }


}
