namespace PoS.WebApi.Application.Services.ItemGroup.Contracts;

using Domain.Entities;

public class ItemGroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}