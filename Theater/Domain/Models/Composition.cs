namespace Domain.Models;

public class Composition
{
    public int Id { get; private init; }
    public string Name { get; private init; }
    public string Description { get; private init; }
    public string CharactersInfo { get; private init; }

    public int AuthorId { get; private init; }

    [System.Text.Json.Serialization.JsonIgnore]
    public Author Author { get; private init; }

    [System.Text.Json.Serialization.JsonIgnore]
    public List<Play> Plays { get; private init; } = new List<Play>();

    public Composition( string name, string description, string charactersInfo, int authorId )
    {
        Name = name;
        Description = description;
        CharactersInfo = charactersInfo;
        AuthorId = authorId;
    }
}