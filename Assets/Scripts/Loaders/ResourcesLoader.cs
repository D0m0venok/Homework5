using System.Linq;
using GameEngine;

public sealed class ResourcesLoader : SaveLoader<ResourcesData, ResourceService>
{
    protected override ResourcesData ConvertToData(ResourceService service)
    {
        return new ResourcesData
        {
            Resources = service.GetResources().ToDictionary(r => r.ID, r => r.Amount)
        };
    }
    protected override void SetData(ResourcesData data, ResourceService service)
    {
        var resources = service.GetResources();
        foreach (var resource in resources)
        {
            resource.Amount = data.Resources[resource.ID];
        }
    }
}