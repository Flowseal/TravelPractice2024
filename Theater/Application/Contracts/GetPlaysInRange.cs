namespace Application.Contracts;

public class GetPlaysInRange
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<PlayBriefInfo> PlayBriefInfos { get; set; }
}
