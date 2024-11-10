namespace PoS.WebApi.Application.Services.ItemGroup.Contracts;

using Domain.Entities;

public class ItemGroupDto
{
    public string Name { get; set; }
    public string Description { get; set; }

    public ItemGroup ToDomain()
    {
        return new ItemGroup()
        {
            Name = Name,
            Description = Description
        };
    }
}