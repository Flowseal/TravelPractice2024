namespace Domain.Models;

public class Author
{
    public int Id { get; private init; }
    public string Name { get; private init; }
    public DateOnly BirthDate { get; private init; }

    public List<Composition> Compositions { get; private init; } = new List<Composition>();

    public Author( string name, DateOnly birthDate )
    {
        Name = name;
        BirthDate = birthDate;
    }
}