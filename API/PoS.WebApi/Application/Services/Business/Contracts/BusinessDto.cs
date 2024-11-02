namespace PoS.WebApi.Application.Services.Business.Contracts;

using Domain.Entities;

public class BusinessDto
{
    public string Name { get; set; }

    public string Address { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public ICollection<User> Employees { get; set; }

    public Business ToDomain()
    {
        return new Business()
        {
            Name = Name,
            Address = Address,
            PhoneNumber = PhoneNumber,
            Email = Email,
            Employees = Employees
        };
    }
}