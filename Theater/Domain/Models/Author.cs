namespace Domain.Models;

public class Author
{
    public int Id { get; private init; }
    public string Name { get; private init; }
    public DateTime BirthDate { get; private init; }

    public List<Composition> Compositions { get; private init; } = new List<Composition>();

    public Author( string name, DateTime birthDate )
    {
        Name = name;
        BirthDate = birthDate;
    }
}