namespace DataAccess.Models;

public class Session
{
    public Session(Guid id, Guid login, Guid workerId, Statistic statistic)
    {
        Id = id;
        Login = login;
        WorkerId = workerId;
        Statistic = statistic;
    }

    protected Session() { }

    public Guid Id { get; set; }
    public Guid Login { get; set; }
    public Guid WorkerId { get; set; }
    public virtual Statistic Statistic { get; set; }
}