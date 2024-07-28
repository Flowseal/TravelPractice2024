namespace WebAPI.Contracts;

public class CreateBusinessHours
{
    public byte Day { get; set; }
    public TimeSpan OpenTime { get; set; }
    public TimeSpan CloseTime { get; set; }
    public DateOnly? ValidFrom { get; set; }
    public DateOnly? ValidThrough { get; set; }

    public int TheaterId { get; set; }
}