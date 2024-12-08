namespace PoS.WebApi.Application.Services.User.Contracts;

public class ShiftDto 
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }
    
    public string StartTime { get; set; }
    
    public string EndTime { get; set; }
}