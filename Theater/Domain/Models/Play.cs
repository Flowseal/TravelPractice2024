using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Play
{
    public int Id { get; private init; }
    public string Name { get; private init; }
    public string Description { get; private init; }
    public DateTime StartDate { get; private init; }
    public DateTime EndDate { get; private init; }

    [Column( TypeName = "decimal(10,2)" )]
    public decimal Price { get; private init; }

    public int TheaterId { get; private init; }

    [System.Text.Json.Serialization.JsonIgnore]
    public Theater Theater { get; private init; }

    public int CompositionId { get; private init; }

    [System.Text.Json.Serialization.JsonIgnore]
    public Composition Composition { get; private init; }

    public Play( string name, string description, DateTime startDate, DateTime endDate,
        decimal price, int theaterId, int compositionId )
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Price = price;
        TheaterId = theaterId;
        CompositionId = compositionId;
    }
}