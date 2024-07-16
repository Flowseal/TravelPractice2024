namespace Domain.Models;

public class BusinessHours
{
    /*
    Идея в том, что у нас будут записи вида:
    - Театр в понедельник(1) работает с 08:00:00 до 17:00:00. 
    - Театр во вторник(2) работает с ...

    С помощью такой структуры мы можем делать, например, обеденные перерывы:
    - Театр в среду(3) работает с 08:00:00 до 12:30:00
    - Театр в среду(3) работает с 14:00:00 до 17:00:00
    
    А также, с помощью аттрибутов ValidFrom (валидно с..) и ValidThrough (валидно до..),
    можно делать праздничные дни. Например, 8 марта в понедельник мы хотим закончить на час раньше:
    - Театр в понедельник(1) работает с 8:00:00 до 17:00:00
    - Театр в понедельник(1) работает с 8:00:00 до 17:00:00 с 8 марта до 8 марта    <-- в приоритете
    */
    public int Id { get; private init; }
    public byte Day { get; private init; }
    public TimeSpan OpenTime { get; private init; }
    public TimeSpan CloseTime { get; private init; }
    public DateTime? ValidFrom { get; private init; }
    public DateTime? ValidThrough { get; private init; }

    public int TheaterId { get; private set; }
    public Theater Theater { get; private set; }

    public BusinessHours( byte day, TimeSpan openTime, TimeSpan closeTime,
        DateTime? validFrom = null, DateTime? validThrough = null )
    {
        Day = day;
        OpenTime = openTime;
        CloseTime = closeTime;
        ValidFrom = validFrom;
        ValidThrough = validThrough;
    }
}