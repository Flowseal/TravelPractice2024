namespace Application.Contracts;

public class CreatePlay
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }

    public int TheaterId { get; set; }
    public int CompositionId { get; set; }
}