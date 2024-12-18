namespace PoS.WebApi.Application.Services.Refund.Contracts;

public class RefundDto
{
    public Guid Id { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public string Reason { get; set; }

    public Guid OrderId { get; set; }
}