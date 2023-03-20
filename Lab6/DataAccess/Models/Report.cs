namespace DataAccess.Models;

public class Report
{
    public Report(Guid id, Statistic statistic)
    {
        Id = id;
        Statistic = statistic;
    }

    protected Report() { }

    public Guid Id { get; set; }
    public virtual Statistic Statistic { get; set; }
}