namespace PoS.WebApi.Application.Services.Service.Contracts;

public class GetAvailableTimesResponse
{
    public IEnumerable<AvailableTimeDto> AvailableTimes { get; set; }
}