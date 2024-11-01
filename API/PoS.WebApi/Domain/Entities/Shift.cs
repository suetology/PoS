using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Domain.Entities;

public class Shift : Entity
{
    public DateTime Date { get; set; }
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }
    
    public Guid EmployeeId { get; set; }
    
    public User Employee { get; set; }
}