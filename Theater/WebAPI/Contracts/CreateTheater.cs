namespace WebAPI.Contracts;

public class CreateTheater
{
    public string Name { get; set; }
    public DateTime OpeningDate { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }
}