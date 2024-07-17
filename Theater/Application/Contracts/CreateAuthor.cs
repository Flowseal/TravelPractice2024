namespace Application.Contracts;

public class CreateAuthor
{
    public string Name { get; set; }
    public DateOnly BirthDate { get; set; }
}