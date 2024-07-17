namespace Application.Contracts;

public class CreateComposition
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string CharactersInfo { get; set; }

    public int AuthorId { get; set; }
}