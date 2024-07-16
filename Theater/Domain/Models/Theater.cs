namespace Domain.Models;

public class Theater
{
    public int Id { get; private init; }
    public string Name { get; private set; }
    public DateTime OpeningDate { get; private init; }
    public string Address { get; private init; }
    public string Description { get; private set; }
    public string PhoneNumber { get; private set; }

    public List<BusinessHours> BusinessHours { get; private init; } = new List<BusinessHours>();

    // TODO plays list

    public Theater( string name, DateTime openingDate, string address, string description, string phoneNumber )
    {
        Name = name;
        OpeningDate = openingDate;
        Address = address;
        Description = description;
        PhoneNumber = phoneNumber;
    }
}